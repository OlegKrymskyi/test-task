using AngelCo.Domain;
using AngelCo.Parser.Extensions;
using AngelCo.Parser.Objects;
using AngelCo.Repositories;
using AngleCo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AngelCo.Parser
{
    class Program
    {
        private static IServiceCollection services;

        private static IServiceProvider provider;

        private static IConfigurationRoot configuration;

        static void Main(string[] args)
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            configuration = builder.Build();

            InitializeIoC();

            provider = services.BuildServiceProvider();

            var parserSettings = provider.GetService<IOptions<ParserOptions>>();
            var userService = provider.GetService<UserService>();

            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            driver.Navigate().GoToUrl("https://angel.co/");
            driver.Navigate().GoToUrl("https://angel.co/login");

            var emailTextBox = driver.FindElement(By.Id("user_email"));
            emailTextBox.Clear();
            emailTextBox.SendKeys(parserSettings.Value.UserEmail);

            var passwordTextBox = driver.FindElement(By.Id("user_password"));
            passwordTextBox.Clear();
            passwordTextBox.SendKeys(parserSettings.Value.UserPassword);

            var loginButton = driver.FindElement(By.Name("commit"));
            loginButton.Click();

            var pageIndex = 0;

            IReadOnlyList<IWebElement> usersRows = null;
            do
            {
                driver.Navigate().GoToUrl($"https://angel.co/people/all?page={pageIndex}");
                usersRows = driver.FindElements(By.CssSelector(".all_rows .item"));
                var usersToProcess = new List<User>();

                foreach (var row in usersRows)
                {
                    var profileLinkAnchor = row.FindElement(By.ClassName("profile-link"));

                    var externalId = profileLinkAnchor.GetAttribute("data-id");

                    var user = userService.GetByExternalId(externalId);
                    if (user == null)
                    {
                        usersToProcess.Add(new User
                        {
                            ExternalId = externalId,
                            ProfileLink = profileLinkAnchor.GetAttribute("href"),
                            AvatarUrl = row.FindElement(By.ClassName("angel_image")).GetAttribute("src")
                        });
                    }
                }

                foreach(var user in usersToProcess)
                { 
                    driver.Navigate().GoToUrl(user.ProfileLink);

                    var header = driver.FindElement(By.TagName("h1"));
                    user.FullName = header.Text.Replace("Report this profile", string.Empty).Trim();

                    var bio = driver.FindElements(ByExtension.AttributeValue("data-field", "bio")).FirstOrDefault();
                    if (bio != null)
                    {
                        user.Bio = bio.Text;
                    }

                    var linkedIn = driver.FindElements(ByExtension.AttributeValue("data-field", "linkedin_url")).FirstOrDefault();
                    if (linkedIn != null)
                    {
                        user.LinkedInUrl = linkedIn.GetAttribute("href");
                    }

                    var twitter = driver.FindElements(ByExtension.AttributeValue("data-field", "twitter_url")).FirstOrDefault();
                    if (twitter != null)
                    {
                        user.TwitterUrl = twitter.GetAttribute("href");
                    }

                    var facebook = driver.FindElements(ByExtension.AttributeValue("data-field", "facebook_url")).FirstOrDefault();
                    if (facebook != null)
                    {
                        user.FacebookUrl = facebook.GetAttribute("href");
                    }

                    var experienceSection = driver.FindElements(ByExtension.AttributeValue("data-module_name", "experience")).FirstOrDefault();
                    if (experienceSection != null)
                    {
                        var experienceDataSource = experienceSection.FindElement(ByExtension.AttributeValue("data-source", "experience"));
                        if (experienceDataSource != null)
                        {
                            var experienceJson = experienceDataSource.GetAttribute("data-roles");
                            if (!string.IsNullOrWhiteSpace(experienceJson))
                            {
                                var experienceObjects = JsonConvert.DeserializeObject<ExperienceObject[]>(experienceJson);

                                user.Experiences = experienceObjects.Select(x => new Experience()
                                {
                                    CompanyName = x.StartupCompanyName,
                                    Title = x.Title,
                                    Role = x.Role,
                                    StartAtYear = x.DatesForSelect?.StartAt.Year,
                                    StartAtMonth = x.DatesForSelect?.StartAt.Month,
                                    EndedAtYear = x.DatesForSelect?.EndedAt.Year,
                                    EndedAtMonth = x.DatesForSelect?.EndedAt.Month
                                }).ToArray();
                            }
                        }
                    }

                    var educationSection = driver.FindElements(ByExtension.AttributeValue("data-module_name", "education")).FirstOrDefault();
                    if (educationSection != null)
                    {
                        var educationDataSource = educationSection.FindElement(By.CssSelector(".profile-module")).FindElements(By.XPath(".//*")).FirstOrDefault();
                        if (educationDataSource != null)
                        {
                            var educationJson = educationDataSource.GetAttribute("data-taggings");
                            if (!string.IsNullOrWhiteSpace(educationJson))
                            {
                                var educationObjects = JsonConvert.DeserializeObject<EducationObject[]>(educationJson);

                                user.Educations = educationObjects.Select(x => new Education()
                                {
                                    DegreeType = x.DegreeType,
                                    Description = x.Description,
                                    FullDegreeName = x.FullDegreeName,
                                    Name = x.Name,
                                    GraduationMonth = x.GraduationMonth,
                                    GraduationYear = x.GraduationYear
                                }).ToArray();
                            }
                        }
                    }

                    var locationsSection = driver.FindElements(ByExtension.AttributeValue("data-field", "tags_interested_locations")).FirstOrDefault();
                    if (locationsSection != null)
                    {
                        var locations = new List<Location>();

                        foreach (var locationLink in locationsSection.FindElements(By.XPath(".//span/a")))
                        {
                            locations.Add(new Location
                            {
                                Name = locationLink.Text
                            });
                        }

                        user.Locations = locations;
                    }

                    var marketsSection = driver.FindElements(ByExtension.AttributeValue("data-field", "tags_interested_markets")).FirstOrDefault();
                    if (marketsSection != null)
                    {
                        var markets = new List<Market>();

                        foreach (var marketLink in marketsSection.FindElements(By.XPath(".//span/a")))
                        {
                            markets.Add(new Market
                            {
                                Name = marketLink.Text
                            });
                        }

                        user.Markets = markets;
                    }

                    userService.Save(user);
                }

                pageIndex++;
            }
            while (usersRows != null || usersRows.Count == 0);

            driver.Close();
            driver.Quit();
        }

        private static void InitializeIoC()
        {
            services = new ServiceCollection();

            services.Configure<ParserOptions>(configuration.GetSection("Parser"));
            services.AddDbContext<DomainContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Database"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Location>, Repository<Location>>();
            services.AddScoped<IRepository<Market>, Repository<Market>>();
            services.AddScoped<IRepository<Education>, Repository<Education>>();
            services.AddScoped<IRepository<Experience>, Repository<Experience>>();

            services.AddScoped<UserService>();
        }
    }
}
