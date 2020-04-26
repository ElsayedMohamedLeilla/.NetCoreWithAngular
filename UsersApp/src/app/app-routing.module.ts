import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsersComponent } from './users/users.component';
import { UserComponent } from './user/user.component';
import { UserAddEditComponent } from './user-add-and-edit/user-add-edit.component';

const routes: Routes = [
  { path: '', component: UsersComponent, pathMatch: 'full' }, // we will load the UsersComponent on the app start page
  { path: 'user/:id', component: UserComponent },
  { path: 'add', component: UserAddEditComponent },
  { path: 'user/edit/:id', component: UserAddEditComponent },
  { path: '**', redirectTo: '/' } // invalid paths for the application will be redirected to the start page
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
