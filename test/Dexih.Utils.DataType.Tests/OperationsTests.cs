using Xunit;

namespace Dexih.Utils.DataType.Tests
{
    public class OperationsTests
    {
        [Fact]
        void Operations_Test()
        {
            var intOperations = Operations<int>.Default;
            var result = intOperations.Add(2, 5);
            
            Assert.Equal(7, result);
        }
    }
}