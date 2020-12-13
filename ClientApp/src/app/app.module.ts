import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { JobsComponent } from './jobs/jobs.component';
import { JobSkillWeightComponent } from './job-skill-weight/job-skill-weight.component';
import { CandidateMatchComponent } from './candidate-match/candidate-match.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    JobsComponent,
    JobSkillWeightComponent,
    CandidateMatchComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: JobsComponent, pathMatch: 'full' },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
