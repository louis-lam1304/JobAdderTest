# JobAdderTest
JobAdder Coding Challenge 

## Main objective

For this exercise, we’d like you to create a web application that will help a recruiter automatically match candidates to open jobs.

## Pre-requisite:
- git
- nodejs (I used v14.15.1)
- python (I used v3.8.6)
- visual studio 2019

## Build Instructions
- git clone https://github.com/louis-lam1304/JobAdderTest.git
- open JobAdderTest.sln in visual studio 2019
- Build and Run (F5)

Below is my thought process / tasks I thought I might share on how I might tackle this problem. At this time, I've already conceptually thought on how to build the solution. 
It's now a matter of learning and building. It will evolve over time and it probably won't be in order upon completion of this coding challenge. 

Since this is my first ASP.NET Core Web Application w/ Angular project, in between these tasks are probably a lot of googling and reading articles on how to tackle a problem. 
All this will come more fluidly with time and experience.
- start visual studio boilerplate project for ASP.NET Core Web Application/Angular project

## Assumptions
- Only implemented this for a single recruiter. Would need to create a sign up/login and have multiple jobskillweights.json file per registered recruiter.
- Coding challenge said find a candidate. I decided just to list all candidates (excluding <= 0 JobWeight) descending based on their JobWeight.
- Services calls are always up, I did not build for any failures from services.
- No data validation on input when adjusting Job Skill Weights
- Did not implement a caching layer. It would definitely help with performance and resilience of system if services go down.

## Matching Algoritm
Very simple solution in which we intersect to Job Skills with the Candidate Skills and Sum up the tags. 
We do have a JobSkillWeight object to allow recruiters to modify skills tags values if they feel some tags are worth higher than others.
```
e.g. 
If JobA had skills requirements of a, b, c, d, e

Weight of Skills 
 - a => 1
 - b => 2
 - c => 3
 - d => 4
 - e => 5

If CandidateA had skills of a, c, e: Weight = Sum(1, 3, 5)
If CandidateB had skills of b, d: Weight = Sum(2, 4)
```
I was considering different variations of this such as 
 - minus the weight if skills if they were missing
 - common words such as `reliable` do not count towards the total. Hence there is a common flag in the JobSkillWeight object. 


## Baseline Implementation

### Back-End
- build models for jobs/candidates
- build service to connect to jobadder api
- build api controller to get jobs/candidates from service
- build data repo for jobskillsweight
  - static json file with all the skills on the jobs
  - using this json file primarly as a database. 
  - allow this file to be read/write
- build models/controllers/services for jobskillsweight
- implement algorithm to find best candidate for job
  - most likely the sum of the interaction of job skills and candidate skills modified by the jobskillsweight

### API Layer
- Job(s) - (/api/job/{id?})
- Candidate(s) - (/api/candidate/{id?})
- JobSkillWeight - (/api/jobskillweight)
- JobSkillWeight/Adjust [POST] - (/api/jobskillweight/adjust) 
```
Example following payload in the body
{
    "name": "dental-assisting",
    "weight": 10,
    "common": true
}
```
- CandidateMatch - /api/candidate/match/{jobId}

### Front-End
- two-column layout (30/70 responsive layout), with header probably.
- three major components
  - jobskillsweight modifer: allows a recruiter to change values of skills
  - jobs listing (left column)
    - onclick action invokes call to candidate match api to find best matched candidates
  - candidate listing (right column)
    - ordered by the algorithm decending.
    - visual helper: green skill tags for matching
    - show missing skills
    
### Potential Improvements
- [x] candidate skills tags has duplicate data, clean up, use another property.
- [ ] make jobadder service url configurable per environment.
- [ ] generate jobskillweights.json dynamically if it does not exist.
- [x] remove candidates from match feed if jobWeight = 0
- [x] error catching on jobweightskill post feed.
- [x] remove boilerplate weatherforecast code from source.
- [x] After reading (https://docs.microsoft.com/en-us/aspnet/core/performance/performance-best-practices?view=aspnetcore-5.0), change controllers to be async.
- [ ] Implement cache layer on jobskillweight service.
- [ ] Move service calls from components to services files in angular
- [ ] create global config to manage potentially adjust scoring mechanisms
- [ ] for mobile: job candidate match view make left side collapsable 
- [ ] add unit tests for MatchService Calculations
- [ ] better visualization of what job has been selected, especially when you scroll down the list.
