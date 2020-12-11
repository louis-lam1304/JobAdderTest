using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JobAdderTest.Models;

namespace JobAdderTest.Services
{
    public interface IJobAdderService
    {
        Task<IEnumerable<Job>> GetJobs();
        Task<IEnumerable<Candidate>> GetCandidates();
    }

    public class JobAdderService : IJobAdderService
    {
        private HttpClient _client { get; }

        public JobAdderService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            var response = await _client.GetAsync("jobs");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<IEnumerable<Job>>();

            return result;
        }

        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            var response = await _client.GetAsync("candidates");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<IEnumerable<Candidate>>();

            return result;
        }
    }
}
