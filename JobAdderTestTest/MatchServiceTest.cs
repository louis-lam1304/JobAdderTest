using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;
using JobAdderTest.Models;
using JobAdderTest.Services;

namespace JobAdderTestTest
{
    public class DependencySetupFixture
    {
        public DependencySetupFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IMatchService, MatchService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        public ServiceProvider ServiceProvider { get; private set; }
    }

    public class MatchServiceTest : IClassFixture<DependencySetupFixture>
    {
        private ServiceProvider _serviceProvider;

        public MatchServiceTest(DependencySetupFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void TestCalculateMethod()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<IMatchService>();

                Job mockJob = new Job { Skills = "a, b, c, d, e" };
                WeightedCandidate mockWeightedCandidate = new WeightedCandidate(new Candidate { SkillTags = "a, c, e" });
                WeightedCandidate mockWeightedCandidate2 = new WeightedCandidate(new Candidate { SkillTags = "b, d" });
                JobSkillWeight[] mockJobSkillWeights = new[]
                {
                     new JobSkillWeight { Name = "a", Weight = 1 },
                     new JobSkillWeight { Name = "b", Weight = 2 },
                     new JobSkillWeight { Name = "c", Weight = 3 },
                     new JobSkillWeight { Name = "d", Weight = 4 },
                     new JobSkillWeight { Name = "e", Weight = 5 },
                };

                var mwc1 = context.Calculate(mockJob, mockWeightedCandidate, mockJobSkillWeights);
                var mwc2 = context.Calculate(mockJob, mockWeightedCandidate2, mockJobSkillWeights);

                Assert.Equal(9, mwc1.JobWeight);
                Assert.Equal(6, mwc2.JobWeight);
            }
        }
    }
}
