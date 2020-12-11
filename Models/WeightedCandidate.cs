using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace JobAdderTest.Models
{
    public class WeightedCandidate : Candidate, IComparable<WeightedCandidate>
    {
        public WeightedCandidate(Candidate c)
        {
            this.CandidateId    = c.CandidateId;
            this.Name           = c.Name;
            this.SkillTags      = c.SkillTags;
        }

        public int JobWeight { get; set; }

        public List<string> FoundSkills { get; set; }

        public List<string> MissingSkills { get; set; }

        public int CompareTo([AllowNull] WeightedCandidate other)
        {
            if (other == null) return 1;

            return other.JobWeight.CompareTo(this.JobWeight);
        }
    }
}
