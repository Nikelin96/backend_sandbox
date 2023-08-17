using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;

namespace GrpcBackendService.UnitsOfWork;
public sealed class CreateSkillJourney
{
    private readonly StatRepository _statRepository;
        private readonly SkillRepository _skillRepository;

    public CreateSkillJourney(StatRepository statRepository,SkillRepository skillRepository)
    {
        _statRepository = statRepository;
        _skillRepository = skillRepository;
    }

    public async Task CreateSkill(Stat stat, Skill skill)
    {
        try
        {
            var statId = await _statRepository.Create(stat);
            skill.StatId = statId;
            var skillId = await _skillRepository.Create(skill);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}