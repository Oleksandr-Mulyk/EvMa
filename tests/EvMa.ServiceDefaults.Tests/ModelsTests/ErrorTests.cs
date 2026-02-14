using EvMa.ServiceDefaults.Models;

namespace EvMa.ServiceDefaults.Tests.ModelsTests
{
    public class ErrorTests
    {
        [Fact]
        public void ErrorNone_ShouldHaveEmptyCodeAndDescription()
        {
            // Assert
            Assert.Equal(string.Empty, Error.None.Code);
            Assert.Equal(string.Empty, Error.None.Description);
        }

        [Fact]
        public void ErrorNullValue_ShouldHaveCorrectDefaults()
        {
            // Assert
            Assert.Equal("Error.NullValue", Error.NullValue.Code);
            Assert.NotEmpty(Error.NullValue.Description);
        }
    }
}
