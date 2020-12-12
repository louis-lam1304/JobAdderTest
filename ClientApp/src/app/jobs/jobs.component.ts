import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Job } from '../shared/models/job';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css']
})
export class JobsComponent implements OnInit {
  public jobs: Job[];

  jobId = 0;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    console.log(baseUrl);
    http.get<Job[]>(baseUrl + 'api/job').subscribe(result => {
      this.jobs = result;
    }, error => console.error(error));
  }
  ngOnInit() {
  }

  public showCandidateMatch(job) {
    console.log(job);
    this.jobId = job.jobId;
  }

}
