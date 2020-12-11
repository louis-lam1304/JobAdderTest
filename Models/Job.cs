using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobAdderTest.Models
{
    public class Job
    {
        public int JobId{ get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public string Skills { internal get; set; }

        private List<string> _uniqueSkills = null;
        public List<string> UniqueSkills
        {
            get
            {
                if (_uniqueSkills == null)
                    _uniqueSkills = Skills.Split(',').Select(s => s.Trim()).Distinct().ToList();

                return _uniqueSkills;
            }
        }

    }
}
