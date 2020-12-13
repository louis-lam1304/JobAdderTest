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
    public class CandidateController : ControllerBase
    {
        private readonly ILogger<CandidateController> _logger;
        private readonly IJobAdderService _jobAdderService;
        private readonly IMatchService _matchService;
        private readonly JobSkillWeightService _jobSkillWeightService;

        public CandidateController(ILogger<CandidateController> logger, IJobAdderService jobAdderService, IMatchService matchService, JobSkillWeightService jobSkillWeightService)
        {
            _logger = logger;
            _jobAdderService = jobAdderService;
            _matchService = matchService;
            _jobSkillWeightService = jobSkillWeightService;
        }

        [HttpGet("{id:int?}")]
        public async Task<IEnumerable<Candidate>> Get(int? id = null)
        {
            return await _jobAdderService.GetCandidates(id);
        }

        [HttpGet("match/{jobId}")]
        public async Task<IEnumerable<WeightedCandidate>> GetMatch(int jobId)
        {
            var jobs = await _jobAdderService.GetJobs(jobId);
            var job = jobs.FirstOrDefault();

            if (job == null) return new List<WeightedCandidate>();

            var candidates = await _jobAdderService.GetCandidates(null);
            var jw = await _jobSkillWeightService.GetJobSkillWeights();

            var wc = candidates.Select(c => new WeightedCandidate(c)).ToList();
            wc.ForEach(c => 
            {
                _matchService.Calculate(job, c, jw);
            });

            wc.RemoveAll(c => c.JobWeight <= 0);
            wc.Sort();

            return wc;
        }

    }
}
