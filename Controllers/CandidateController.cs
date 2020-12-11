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
    [Route("[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ILogger<CandidateController> _logger;
        private readonly IJobAdderService _jobAdderService;

        public CandidateController(ILogger<CandidateController> logger, IJobAdderService jobAdderService)
        {
            _logger = logger;
            _jobAdderService = jobAdderService;
        }

        [HttpGet("{id:int?}")]
        public IEnumerable<Candidate> Get(int? id = null)
        {
            var results = _jobAdderService.GetCandidates().Result;

            if (id.HasValue)
                return results.Where(c => c.CandidateId == id);

            return results;
        }
    }
}
