import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JobSkillWeight } from '../shared/models/job-skill-weight';

@Component({
  selector: 'app-job-skill-weight',
  templateUrl: './job-skill-weight.component.html',
  styleUrls: ['./job-skill-weight.component.css']
})
export class JobSkillWeightComponent implements OnInit {

  public jobSkillWeights: JobSkillWeight[];
  public selectedJSW: JobSkillWeight;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    http.get<JobSkillWeight[]>(baseUrl + 'api/jobskillweight').subscribe(result => {
      this.jobSkillWeights = result;
      this.selectedJSW = this.jobSkillWeights[0];
    }, error => console.error(error));
  }

  ngOnInit() {
    
  }

  update() {
    console.log(this.selectedJSW);
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    this.http.post(this.baseUrl + 'api/jobskillweight/adjust', this.selectedJSW, { headers: headers }).subscribe(
      data => console.log(data)
    );
  }
}
