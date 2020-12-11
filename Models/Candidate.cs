using Microsoft.AspNetCore.Mvc;
using System;

namespace JobAdderTest.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }

        public string Name { get; set; }

        public string SkillTags { get; set; }
    }
}
