import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Job } from '../shared/models/job';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css']
})
export class JobsComponent implements OnInit {

  private refreshEvent: EventEmitter<boolean> = new EventEmitter();

  public jobs: Job[];
  jobId = 0;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Job[]>(baseUrl + 'api/job').subscribe(result => {
      this.jobs = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  showCandidateMatch(job) {
    this.jobId = job.jobId;
  }

  refresh(event) {
    this.refreshEvent.emit(event);
  }

}
