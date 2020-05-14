import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {RouterModule, Routes} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from "./component/dumb/nav-menu/nav-menu.component";
import {EulerProblemComponent} from "./component/smart/euler-problems/euler-problem/euler-problem.component";
import {EulerProblemsComponent} from "./component/smart/euler-problems/euler-problems.component";
import {HomeComponent} from "./component/dumb/home/home.component";

const routes: Routes = [
  {path: '', component: HomeComponent, pathMatch: 'full'},
  {path: 'Problem', component: EulerProblemsComponent},
  {path: 'Problem/:id', component: EulerProblemComponent}
];
const dumbComponents: any[] = [
  NavMenuComponent,
  HomeComponent,
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
    RouterModule.forRoot(routes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
