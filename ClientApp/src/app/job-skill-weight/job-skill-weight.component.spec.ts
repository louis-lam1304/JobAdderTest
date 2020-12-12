import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JobSkillWeightComponent } from './job-skill-weight.component';

describe('JobSkillWeightComponent', () => {
  let component: JobSkillWeightComponent;
  let fixture: ComponentFixture<JobSkillWeightComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JobSkillWeightComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JobSkillWeightComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
