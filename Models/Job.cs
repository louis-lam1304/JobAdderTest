using Microsoft.AspNetCore.Mvc;
using System;

namespace JobAdderTest.Models
{
    public class Job
    {
        public int JobId{ get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public string Skills { get; set; }
    }
}
