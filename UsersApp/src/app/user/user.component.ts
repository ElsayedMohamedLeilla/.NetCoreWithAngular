import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';
import { User } from '../models/user';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  user$: Observable<User>;
  userId: number;

  constructor(private userService: UserService, private avRoute: ActivatedRoute) {
    const idParam = 'id';
    if (this.avRoute.snapshot.params[idParam]) {
      this.userId = this.avRoute.snapshot.params[idParam];
    }
  }

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    this.user$ = this.userService.getUser(this.userId);
  }

  confirm(userId, name) {
    const ans = confirm('Do You Want To Resend Confirmation Email Again To: ' + name);
    if (ans) {
      this.userService.confirmUser(userId).subscribe((data) => {
        alert('Confirmation Email Sent');
        this.loadUser();
      });
    }
  }
}

