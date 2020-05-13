import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {RouterModule, Routes} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {EulerProblemsComponent} from "./euler-problems/euler-problems.component";
import { EulerProblemComponent } from './euler-problems/euler-problem/euler-problem.component';

const routes: Routes = [
  {path: '', component: HomeComponent, pathMatch: 'full'},
  {path: 'Problem', component: EulerProblemsComponent},
];
const dumbComponents: any[] = [
  NavMenuComponent,
];
const smartComponents: any[] = [
  HomeComponent,
  EulerProblemsComponent,
];

@NgModule({
  declarations: [
    AppComponent,
    dumbComponents,
    smartComponents,
    EulerProblemComponent,
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
