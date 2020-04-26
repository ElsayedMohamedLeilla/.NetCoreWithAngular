import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';
import { User } from '../models/user';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  users$: Observable<User[]>;

  constructor(private userService: UserService) {
  }

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
    this.users$ = this.userService.getUsers();
  }

  delete(userId) {
    const ans = confirm('Do you want to delete user with id: ' + userId);
    if (ans) {
      this.userService.deleteUser(userId).subscribe((data) => {
        this.loadUsers();
      });
    }
  }

  confirm(userId, name) {
    const ans = confirm('Do You Want To Resend Confirmation Email Again To: ' + name);
    if (ans) {
      this.userService.confirmUser(userId).subscribe((data) => {
        alert('Confirmation Email Sent');
        this.loadUsers();
      });
    }
  }
}
