import { Component, Input, OnChanges, SimpleChanges, Inject, EventEmitter, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WeightedCandidate } from '../shared/models/weighted-candidate';

@Component({
  selector: 'app-candidate-match',
  templateUrl: './candidate-match.component.html',
  styleUrls: ['./candidate-match.component.css']
})
export class CandidateMatchComponent implements OnChanges, OnInit, OnDestroy  {

  public candidates: WeightedCandidate[];

  @Input() jobId: number;
  @Input() private refreshEvent: EventEmitter<boolean>;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
    if (this.refreshEvent) {
      this.refreshEvent.subscribe(data => {
        this.callMatchService(this.jobId);
      });
    }
  }

  ngOnDestroy() {
    this.refreshEvent.unsubscribe();
  }

  ngOnChanges(changes: SimpleChanges) {
    this.callMatchService(changes.jobId.currentValue);
  }

  callMatchService(id) {
    this.candidates = null;
    this.http.get<WeightedCandidate[]>(`${this.baseUrl}api/candidate/match/${id}`).subscribe(result => {
      this.candidates = result;
    }, error => console.error(error));
  }

}
