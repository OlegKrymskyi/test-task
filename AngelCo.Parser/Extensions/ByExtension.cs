using OpenQA.Selenium;

namespace AngelCo.Parser.Extensions
{
    public static class ByExtension
    {
        public static By AttributeValue(string attrName, string attrValue)
        {
            return By.XPath($"//*[@{attrName} = '{attrValue}']");
        }
    }
}
