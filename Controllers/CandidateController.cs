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
        public IEnumerable<Candidate> Get(int? id = null)
        {
            return _jobAdderService.GetCandidates(id).Result;
        }

        [HttpGet("match/{jobId}")]
        public IEnumerable<WeightedCandidate> GetMatch(int jobId)
        {
            var job = _jobAdderService.GetJobs(jobId).Result.First();

            if (job == null) return new List<WeightedCandidate>();

            var candidates = _jobAdderService.GetCandidates(null).Result;
            var jw = _jobSkillWeightService.GetJobSkillWeights();

            var wc = candidates.Select(c => new WeightedCandidate(c)).ToList();
            wc.ForEach(c => 
            {
                _matchService.Calculate(job, c, jw);
            });

            wc.Sort();

            return wc;
        }

    }
}
