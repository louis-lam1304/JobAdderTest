using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JobAdderTest.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

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

        public async Task<IEnumerable<JobSkillWeight>> GetJobSkillWeights()
        {
            using var reader = File.OpenRead(_jobSkillWeightsFile);
            return await JsonSerializer.DeserializeAsync<JobSkillWeight[]>(reader,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task AdjustJobSkillWeight(JobSkillWeight jobSkillWeight)
        {
            var jobSkillWeights = await GetJobSkillWeights();

            var jsw = jobSkillWeights.FirstOrDefault(j => j.Name == jobSkillWeight.Name);

            if (jsw != null)
            {
                jsw.Weight = jobSkillWeight.Weight;
                jsw.Common = jobSkillWeight.Common;
            }

            using (var fs = File.Create(_jobSkillWeightsFile))
            {
                await JsonSerializer.SerializeAsync<IEnumerable<JobSkillWeight>>(fs, jobSkillWeights, new JsonSerializerOptions { WriteIndented = true });
            }
        }

    }
}
