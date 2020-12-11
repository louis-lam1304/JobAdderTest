using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;

namespace JobAdderTest.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }

        public string Name { get; set; }

        public string SkillTags { get; set; }

        private List<string> _uniqueSkillTags = null;
        public List<string> UniqueSkillTags
        {
            get
            {
                if (_uniqueSkillTags == null)
                    _uniqueSkillTags = SkillTags.Split(',').Select(s => s.Trim()).Distinct().ToList();

                return _uniqueSkillTags;
            }
        }
    }
}
