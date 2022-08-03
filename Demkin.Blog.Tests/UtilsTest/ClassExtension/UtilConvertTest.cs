using Demkin.Blog.Utils.ClassExtension;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Demkin.Blog.Tests.UtilsTest.ClassExtension
{
    public class UtilConvertTest
    {
        [Theory]
        [InlineData("2")]
        [InlineData(2)]
        public void ObjToIntTest(object value)
        {
            var result = UtilConvert.ObjToInt(value);

            Assert.Equal(2, result);
        }
    }
}