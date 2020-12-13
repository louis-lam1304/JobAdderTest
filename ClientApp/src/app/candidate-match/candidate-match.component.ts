import { Component, Input, OnChanges, SimpleChanges, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WeightedCandidate } from '../shared/models/weighted-candidate';

@Component({
  selector: 'app-candidate-match',
  templateUrl: './candidate-match.component.html',
  styleUrls: ['./candidate-match.component.css']
})
export class CandidateMatchComponent implements OnChanges  {

  public candidates: WeightedCandidate[];

  @Input() jobId: number;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  ngOnChanges(changes: SimpleChanges) {
    this.candidates = null;
    this.http.get<WeightedCandidate[]>(`${this.baseUrl}api/candidate/match/${changes.jobId.currentValue}`).subscribe(result => {
      this.candidates = result;
    }, error => console.error(error));
  }
}
