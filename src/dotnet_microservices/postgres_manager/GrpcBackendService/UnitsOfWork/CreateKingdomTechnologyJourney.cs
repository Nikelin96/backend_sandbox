using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;

namespace GrpcBackendService.UnitsOfWork;
public sealed class CreateKingdomTechnologyJourney
{
    private readonly KingdomTechnologyRepository _kingdomTechnologyRepository;
    private readonly KingdomTransactionRepository _kingdomTransactionRepository;
    private readonly TechnologyRepository _technologyRepository;
    private readonly IConnectionCreator _connectionCreator;
    private readonly IDataAccessExecutor _executor;

    public CreateKingdomTechnologyJourney(KingdomTechnologyRepository kingdomTechnologyRepository,
        TechnologyRepository technologyRepository
        , IConnectionCreator connectionCreator, IDataAccessExecutor executor, KingdomTransactionRepository kingdomTransactionRepository)
    {
        _connectionCreator = connectionCreator;
        _executor = executor;
        _kingdomTechnologyRepository = kingdomTechnologyRepository;
        _technologyRepository = technologyRepository;
        _kingdomTransactionRepository = kingdomTransactionRepository;
    }

    public async Task<int> CreateKingdomTechnology(KingdomTechnology kingdomTecnology)
    {
        var technologyPrices = await _technologyRepository.GetPricesForTechnology(kingdomTecnology.TechnologyId.Value);

        if (technologyPrices.Count() == 0)
        {
            throw new KeyNotFoundException("Technology with such id does not exist");
        }

        // check that kingdom_transaction exists, and its value equals to technology price
        var kingdomTransactions = await _kingdomTransactionRepository.GetKingdomTransactionsForKingdomTechnology(kingdomTecnology);

        if (kingdomTransactions.Count() > 0)
        { //we are good

            var woodTotal = technologyPrices.Sum((x)=> x.Wood);
            var foodTotal = technologyPrices.Sum((x)=> x.Food);
            var goldTotal = technologyPrices.Sum((x)=> x.Gold);
            var stoneTotal = technologyPrices.Sum((x)=> x.Stone);

            var checkSum = kingdomTransactions.First();

            if (checkSum.Wood == woodTotal && checkSum.Food == foodTotal && checkSum.Gold == goldTotal && checkSum.Stone == stoneTotal)
            {

                return await _kingdomTechnologyRepository.Create(kingdomTecnology);
            }
        }
        // todo write warning that record was not inserted, because transactions were not found
        return -1;
    }
}