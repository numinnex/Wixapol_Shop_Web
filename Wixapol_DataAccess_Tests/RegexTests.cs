using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Wixapol_DataAccess_Tests
{
    public class RegexTests
    {
        [Theory]
        [InlineData("33-338")]
        [InlineData("1-21")]
        [InlineData("12-12")]
        [InlineData("98-1")]
        public void PostalCodeMatches(string toSearch)
        {
            string pattern = @"^\d{1,2}-\d{1,3}$";

            bool output = Regex.IsMatch(toSearch, pattern);

            Assert.True(output);
        }
        [Theory]
        [InlineData("333-338")]
        [InlineData("12-2121")]
        [InlineData("xD")]
        [InlineData("xd-xdd")]
        [InlineData("1-1243")]
        public void PostalCodeDoesntMatch(string toSearch)
        {
            string pattern = @"^\d{1,2}-\d{1,3}$";

            bool output = Regex.IsMatch(toSearch, pattern);

            Assert.False(output);
        }
        [Theory]
        [InlineData("2y5m")]
        [InlineData("1y12m")]
        [InlineData("6m")]
        [InlineData("1m")]
        [InlineData("12m")]
        [InlineData("12y3m")]
        [InlineData("5y11m")]
        public void WarrantyLengthMatches(string toSearch)
        {
            string pattern = @"(^(([0-9]{1}|[0-2]{2})m$)|(^(\d{1,2}y$)|^\d{1,2}y([0-9]{1}|[0-2]{2})m$))";

            bool output = Regex.IsMatch(toSearch, pattern);

            Assert.True(output);
        }
        [Theory]
        [InlineData("2y13m")]
        [InlineData("233y12m")]
        [InlineData("12y69m")]
        [InlineData("69m")]
        [InlineData("13m")]
        [InlineData("69y13m")]
        [InlineData("133y")]
        public void WarrantyLengthDoesntMatch(string toSearch)
        {
            string pattern = @"(^(([0-9]{1}|[0-2]{2})m$)|(^(\d{1,2}y$)|^\d{1,2}y([0-9]{1}|[0-2]{2})m$))";

            bool output = Regex.IsMatch(toSearch, pattern);

            Assert.False(output);
        }

    }
}
