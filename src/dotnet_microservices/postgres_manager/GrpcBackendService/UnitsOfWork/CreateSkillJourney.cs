
using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace GrpcBackendService.UnitsOfWork;
public sealed class CreateSkillJourney
{
    private readonly ICreateEntityCommand<Stat> _statCreationRepository;
    private readonly ICreateEntityCommand<Skill> _skillCreationRepository;

    public CreateSkillJourney(ICreateEntityCommand<Stat> statCreationRepository, ICreateEntityCommand<Skill> skillCreationRepository)
    {
        _statCreationRepository = statCreationRepository;
        _skillCreationRepository = skillCreationRepository;
    }

    public async Task CreateSkill(Stat stat, Skill skill)
    {
        try
        {
            var statId = await _statCreationRepository.Create(stat);
            skill.StatId = statId;
            var skillId = await _skillCreationRepository.Create(skill);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}