using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JobAdderTest.Models;
using System.Linq;

namespace JobAdderTest.Services
{
    public interface IMatchService
    {
        WeightedCandidate Calculate(Job job, Candidate candidate, IEnumerable<JobSkillWeight> weights);
    }

    public class MatchService : IMatchService
    {
  
        public WeightedCandidate Calculate(Job job, Candidate candidate, IEnumerable<JobSkillWeight> weights)
        {
            var wc = candidate as WeightedCandidate;

            wc.FoundSkills = job.UniqueSkills.Intersect(candidate.UniqueSkillTags).ToList();
            wc.MissingSkills = job.UniqueSkills.Except(candidate.UniqueSkillTags).ToList();

            wc.JobWeight = wc.FoundSkills.Select(c => weights.First(w => w.Name == c).Weight).Sum();

            return wc;
        }
    }
}
