import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MembersComponent } from './members/members.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from '../guards/auth.guard';
import { MemberDetailsComponent } from './members/member-details/member-details.component';
import { MemberDetailsResolver } from '../guards/members/member-details.resolver';
import { MembersListResolver } from '../guards/members/members-list.resolver';

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent,
        pathMatch: 'full'
    },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'members', component: MembersComponent, resolve: { users: MembersListResolver } },
            { path: 'members/:id', component: MemberDetailsComponent, resolve: { user: MemberDetailsResolver } },
            { path: 'lists', component: ListsComponent },
            { path: 'messages', component: MessagesComponent },
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
