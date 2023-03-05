using EmployeeManagement.Common.Dtos.Job;

namespace EmployeeManagement.Common.Interfaces;

public interface IJobService 
{
    Task<int> CreateJobAsync(JobCreate jobCreate);
    Task UpdateJobAsync(JobUpdate jobUpdate);
    Task DeleteJobAsync(JobDelete jobDelete);
    Task<JobGet> GetJobByIdAsync(int id);
    Task<List<JobGet>> GetJobsAsync();
}
