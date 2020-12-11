using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JobAdderTest.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Newtonsoft.Json;

namespace JobAdderTest.Services
{
    public class JobSkillWeightService 
    {
        public IWebHostEnvironment WebHostEnvironment { get; }

        public JobSkillWeightService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string _jobSkillWeightsFile
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "jobskillweights.json"); }
        }

        public IEnumerable<JobSkillWeight> GetJobSkillWeights()
        {
            using (var jsonFileReader = File.OpenText(_jobSkillWeightsFile))
            {
                return System.Text.Json.JsonSerializer.Deserialize<JobSkillWeight[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public void AdjustJobSkillWeight(JobSkillWeight jobSkillWeight)
        {
            var jobSkillWeights = GetJobSkillWeights();

            var jsw = jobSkillWeights.FirstOrDefault(j => j.Name == jobSkillWeight.Name);

            if (jsw != null)
            {
                jsw.Weight = jobSkillWeight.Weight;
                jsw.Common = jobSkillWeight.Common;
            }

            using (var sw = File.CreateText(_jobSkillWeightsFile))
            {
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;

                    var serializer = new Newtonsoft.Json.JsonSerializer();
                    serializer.Serialize(jw, jobSkillWeights);
                }
            }
        }

    }
}
