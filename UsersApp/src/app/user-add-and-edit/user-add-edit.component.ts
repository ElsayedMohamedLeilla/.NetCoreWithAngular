import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../services/user.service';
import { User } from '../models/user';

@Component({
  selector: 'app-user-add-edit',
  templateUrl: './user-add-edit.component.html',
  styleUrls: ['./user-add-edit.component.scss']
})
export class UserAddEditComponent implements OnInit {
  form: FormGroup;
  actionType: string;


  formName: string;
  formlastName: string;
  formusername: string;
  formPassword: string;
  formEmail: string;
  formphoneNumber: string;



  userId: number;
  errorMessage: any;
  existingUser: User;

  constructor(private userService: UserService, private formBuilder:
    // tslint:disable-next-line: align
    FormBuilder, private avRoute: ActivatedRoute, private router: Router) {
    const idParam = 'id';
    this.actionType = 'Add';
    this.formName = 'name';
    this.formlastName = 'lastName';
    this.formusername = 'username';
    this.formPassword = 'password';
    this.formEmail = 'email';
    this.formphoneNumber = 'phoneNumber';


    if (this.avRoute.snapshot.params[idParam]) {
      this.userId = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        userId: 0,
        name: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        username: ['', [Validators.required]],
        password: ['', [Validators.required]],
        email: ['', [Validators.required, Validators.email]],
        phoneNumber: ['', [Validators.required]],
      }
    );
  }

  ngOnInit() {

    if (this.userId > 0) {
      this.actionType = 'Edit';
      this.userService.getUser(this.userId)
        .subscribe(data => (
                   this.existingUser = data,
          this.form.controls[this.formName].setValue(data.name),
          this.form.controls[this.formusername].setValue(data.username),
          this.form.controls[this.formPassword].setValue(data.password),
          this.form.controls[this.formlastName].setValue(data.lastName),
          this.form.controls[this.formEmail].setValue(data.email),
          this.form.controls[this.formphoneNumber].setValue(data.phoneNumber)
        ));
    }
  }

  save() {
    if (!this.form.valid) {
      return;
    }

    if (this.actionType === 'Add') {
      // tslint:disable-next-line: prefer-const
      let user: User = {
        addedDate: new Date(),
        isEmailConfirmed: false,
        emailConfirmationToken: 'temp',
        username: this.form.get(this.formusername).value,
        name: this.form.get(this.formName).value,
        password: this.form.get(this.formPassword).value,
        lastName: this.form.get(this.formlastName).value,
        email: this.form.get(this.formEmail).value,
        phoneNumber: this.form.get(this.formphoneNumber).value,
      };

      this.userService.saveUser(user)
        .subscribe((data) => {
          this.router.navigate(['/user', data.userId]);
        });
    }

    if (this.actionType === 'Edit') {
      // tslint:disable-next-line: prefer-const
      let user: User = {

        userId: this.existingUser.userId,
        addedDate: this.existingUser.addedDate,
        isEmailConfirmed: this.existingUser.isEmailConfirmed,
        emailConfirmationToken: this.existingUser.emailConfirmationToken,


        name: this.form.get(this.formName).value,
        username: this.form.get(this.formusername).value,
        password: this.form.get(this.formPassword).value,
        lastName: this.form.get(this.formlastName).value,
        email: this.form.get(this.formEmail).value,
        phoneNumber: this.form.get(this.formphoneNumber).value


      };
      this.userService.updateUser(user.userId, user)
        .subscribe((data) => {
          this.router.navigate(['/user', user.userId]);
        });
    }
  }

  cancel() {
    this.router.navigate(['/']);
  }

  get name() { return this.form.get(this.formName); }
  get username() { return this.form.get(this.formusername); }
  get lastName() { return this.form.get(this.formlastName); }
  get password() { return this.form.get(this.formPassword); }
  get email() { return this.form.get(this.formEmail); }
  get phoneNumber() { return this.form.get(this.formphoneNumber); }
}
