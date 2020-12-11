using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JobAdderTest.Models;
using System.Linq;

namespace JobAdderTest.Services
{
    public interface IJobAdderService
    {
        Task<IEnumerable<Job>> GetJobs(int? id);
        Task<IEnumerable<Candidate>> GetCandidates(int? id);
    }

    public class JobAdderService : IJobAdderService
    {
        private HttpClient _client { get; }

        public JobAdderService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Job>> GetJobs(int? id = null)
        {
            var response = await _client.GetAsync("jobs");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<IEnumerable<Job>>();

            if (id.HasValue)
                return result.Where(r => r.JobId == id);

            return result;
        }

        public async Task<IEnumerable<Candidate>> GetCandidates(int? id = null)
        {
            var response = await _client.GetAsync("candidates");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<IEnumerable<Candidate>>();

            if (id.HasValue)
                return result.Where(r => r.CandidateId== id);

            return result;
        }
    }
}
