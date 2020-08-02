import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';

import { HttpClientModule } from '@angular/common/http';
import { NavComponent } from './nav/nav.component';

import { ErrorInterceptorProvider } from '../interceptors/error.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './home/home.component';
import { MembersComponent } from './members/members.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';

import { routes } from './routes';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    MembersComponent,
    ListsComponent,
    MessagesComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    RouterModule.forRoot(routes)
  ],
  providers: [
    ErrorInterceptorProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
