using EmployeeManagement.Common.Dtos.Team;

namespace EmployeeManagement.Common.Interfaces;

public interface ITeamService
{
    Task<int> CreateTeamAsync(TeamCreate teamCreate);
    Task UpdateTeamAsync(TeamUpdate teamUpdate);
    Task DeleteTeamAsync(TeamDelete teamDelete);
    Task<TeamGet> GetTeamByIdAsync(int id);
    Task<List<TeamGet>> GetTeamsAsync();
}
