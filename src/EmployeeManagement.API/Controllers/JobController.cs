using EmployeeManagement.Common.Dtos.Job;
using EmployeeManagement.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    private IJobService JobService { get; }

    public JobController(IJobService jobService)
    {
        JobService = jobService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateJob (JobCreate JobCreate)
    {
        var id = await JobService.CreateJobAsync(JobCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateJob (JobUpdate JobUpdate)
    {
        await JobService.UpdateJobAsync(JobUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteJob (JobDelete JobDelete)
    {
        await JobService.DeleteJobAsync(JobDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetJobById(int id)
    {
        var Job = await JobService.GetJobByIdAsync(id);
        return Ok(Job);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetJobs()
    {
        var Jobes = await JobService.GetJobsAsync();
        return Ok(Jobes);
    }
    
}
