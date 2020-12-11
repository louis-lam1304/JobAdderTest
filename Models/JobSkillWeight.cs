using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace JobAdderTest.Models
{
    public class JobSkillWeight
    {
        public string Name { get; set; }

        public int Weight { get; set; }

        /* Different behaviour for common words, maybe they are ignored in the matching algorithm. */
        public bool Common { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<JobSkillWeight>(this);
        }
    }
}
