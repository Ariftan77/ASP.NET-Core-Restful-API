using AutoMapper;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.Business.Validation;
using EmployeeManagement.Common.Dtos.Job;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Model;
using FluentValidation;

namespace EmployeeManagement.Business.Services;

public class JobService : IJobService
{
    private IMapper Mapper { get; }
    public IGenericRepository<Job> JobRepository { get; }
    public JobCreateValidator JobCreateValidator { get; }
    public JobUpdateValidator JobUpdateValidator { get; }

    public JobService(IMapper mapper, IGenericRepository<Job> jobRepository,
        JobCreateValidator jobCreateValidator,
        JobUpdateValidator jobUpdateValidator)
    {
        Mapper = mapper;
        JobRepository = jobRepository;
        JobCreateValidator = jobCreateValidator;
        JobUpdateValidator = jobUpdateValidator;
    }
    public async Task<int> CreateJobAsync(JobCreate jobCreate)
    {
        await JobCreateValidator.ValidateAndThrowAsync(jobCreate);

        var entity = Mapper.Map<Job>(jobCreate);

        await JobRepository.InsertAsync(entity);
        await JobRepository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteJobAsync(JobDelete jobDelete)
    {
        var entity = await JobRepository.GetByIdAsync(jobDelete.id, includes: (job) => job.Employees);

        if (entity == null)
            throw new JobNotFoundException(jobDelete.id);

        if (entity.Employees.Count > 0)
            throw new DependentEmployeeExistException(entity.Employees);

        JobRepository.Delete(entity);
        await JobRepository.SaveChangesAsync();
    }

    public async Task<JobGet> GetJobByIdAsync(int id)
    {
        var entity = await JobRepository.GetByIdAsync(id);

        if (entity == null)
            throw new JobNotFoundException(id);

        return Mapper.Map<JobGet>(entity);
    }

    public async Task<List<JobGet>> GetJobsAsync()
    {
        var entities = await JobRepository.GetAsync(null, null);
        return Mapper.Map<List<JobGet>>(entities);
    }

    public async Task UpdateJobAsync(JobUpdate jobUpdate)
    {
        await JobUpdateValidator.ValidateAndThrowAsync(jobUpdate);

        var existingEntity = await JobRepository.GetByIdAsync(jobUpdate.Id,isTracking: false);

        if (existingEntity == null)
            throw new JobNotFoundException(jobUpdate.Id);

        var entity = Mapper.Map<Job>(jobUpdate);

        JobRepository.Update(entity);
        await JobRepository.SaveChangesAsync();
    }
}
