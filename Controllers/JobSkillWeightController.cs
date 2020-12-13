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
    public class JobSkillWeightController : ControllerBase
    {
        private readonly ILogger<JobSkillWeightController> _logger;
        private readonly JobSkillWeightService _jobSkillWeightService;

        public JobSkillWeightController(ILogger<JobSkillWeightController> logger, JobSkillWeightService jobSkillWeightService)
        {
            _logger = logger;
            _jobSkillWeightService = jobSkillWeightService;
        }

        [HttpGet]
        public async Task<IEnumerable<JobSkillWeight>> Get()
        {
            return await _jobSkillWeightService.GetJobSkillWeights();
        }

        [HttpPost("adjust")]
        public async Task<ActionResult> Adjust([FromBody] JobSkillWeight jobSkillWeightRequest)
        {
            try
            {
                await _jobSkillWeightService.AdjustJobSkillWeight(jobSkillWeightRequest);
                return Ok(jobSkillWeightRequest);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
