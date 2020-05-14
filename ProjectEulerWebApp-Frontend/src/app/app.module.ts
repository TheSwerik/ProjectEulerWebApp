import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule, Routes} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './component/dumb/nav-menu/nav-menu.component';
import {EulerProblemComponent} from './component/smart/euler-problems/euler-problem/euler-problem.component';
import {EulerProblemsComponent} from './component/smart/euler-problems/euler-problems.component';
import {HomeComponent} from './component/dumb/home/home.component';
import {LoggerModule, NgxLoggerLevel} from 'ngx-logger';
import {DifficultyPipe} from './util/difficulty.pipe';
import {DatePipe} from './util/date.pipe';
import {SafeHtmlPipe} from './util/safe-html.pipe';

const routes: Routes = [
  {path: '', component: HomeComponent, pathMatch: 'full'},
  {path: 'Problem', component: EulerProblemsComponent},
  {path: 'Problem/:id', component: EulerProblemComponent}
];
const thirdPartyModules: any[] = [
  LoggerModule.forRoot({
    level: NgxLoggerLevel.INFO,
    serverLogLevel: NgxLoggerLevel.ERROR
  }),
];
const dumbComponents: any[] = [
  NavMenuComponent,
  HomeComponent,
  DifficultyPipe,
  DatePipe,
  SafeHtmlPipe,
];
const smartComponents: any[] = [
  EulerProblemsComponent,
  EulerProblemComponent,
];

@NgModule({
  declarations: [
    AppComponent,
    dumbComponents,
    smartComponents,
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes),
    thirdPartyModules
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
