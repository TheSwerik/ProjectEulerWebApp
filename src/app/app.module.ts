import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {RouterModule, Routes} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';

const routes: Routes = [
  {path: '', component: HomeComponent, pathMatch: 'full'},
  {path: 'fetch-data', component: FetchDataComponent},

]
const dumbComponents: any[] = [
  NavMenuComponent,
]
const smartComponents: any[] = [
  HomeComponent,
  FetchDataComponent
]

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
