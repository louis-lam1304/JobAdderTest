using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobAdderTest.Models;
using JobAdderTest.Services;

namespace JobAdderTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IJobAdderService _jobAdderService;

        public JobController(ILogger<JobController> logger, IJobAdderService jobAdderService)
        {
            _logger = logger;
            _jobAdderService = jobAdderService;
        }

        [HttpGet("{id:int?}")]
        public IEnumerable<Job> Get(int? id = null)
        {
            return _jobAdderService.GetJobs(id).Result;
        }
    }
}
