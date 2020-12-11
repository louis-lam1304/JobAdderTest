# JobAdderTest
JobAdder Coding Challenge 

Main objective

For this exercise, we’d like you to create a web application that will help a recruiter automatically match candidates to open jobs.

Pre-requisite:
- git
- nodejs (I used v14.15.1)
- python (I used v3.8.6)
- visual studio 2019


Below is my thought process / tasks I thought I might share on how I might tackle this problem. At this time, I've already conceptually thought on how to build the solution. 
It's now a matter of learning and building. It will evolve over time and it probably won't be in order upon completion of this coding challenge. 

Since this is my first ASP.NET Core Web Application w/ Angular project, in between these tasks are probably a lot of googling and reading articles on how to tackle said problem. 
All this will come more fluidly with time and experience.
- start visual studio boilerplate project for ASP.NET Core Web Application/Angular project

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
    - onclick action invokes algorithm to find best matched candidates
  - candidate listing (right column)
    - ordered by the algorithm decending.
    - visual helper: green skill tags for matching, red for no match
    
### Potential Improvements
- [x] candidate skills tags has duplicate data, clean up, use another property.
- [ ] make jobadder service url configurable per environment.
- [ ] generate jobskillweights.json dynamically if it does not exist.
- [ ] remove candidates from match feed if jobWeight = 0
- [ ] error catching on jobweightskill post feed.
- [ ] remove weatherforecast code from boilerplate.