using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using NSubstitute;
using System.Data;
using Xunit.Abstractions;

namespace GrpcServiceTest;
public class TechnologyDependencyTests
{
    private readonly TechnologyRepository _sut;
    private readonly IDataAccessExecutor _repositoryMock;

    private readonly ITestOutputHelper _outputHelper;

    public TechnologyDependencyTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;

        var connectionCreator = Substitute.For<IConnectionCreator>();
        _repositoryMock = Substitute.For<IDataAccessExecutor>();

        _sut = new TechnologyRepository(connectionCreator, _repositoryMock);
    }

    [Fact]
    public async Task Create_ValidTechnology_ReturnsId()
    {
        var technology = new Technology();
        var expectedSql = @"INSERT INTO technology(name, description, research_time) VALUES (@Name, @Description, @ResearchTime) RETURNING id;";
        _repositoryMock.ExecuteScalarAsync<int>(Arg.Any<IDbConnection>(), expectedSql, technology).Returns(1);

        var resultId =  await _sut.Create(technology);

        Assert.Equal(1, resultId);
    }
}