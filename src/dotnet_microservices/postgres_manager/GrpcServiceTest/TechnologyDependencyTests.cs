using GrpcBackendService.DataAccess;
using Xunit.Abstractions;

namespace GrpcServiceTest
{
    public class TechnologyDependencyTests
    {
        private TechnologyRepository _sut = new TechnologyRepository(null);

        private readonly ITestOutputHelper _outputHelper;

        public TechnologyDependencyTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void Test1()
        {

        }
    }
}