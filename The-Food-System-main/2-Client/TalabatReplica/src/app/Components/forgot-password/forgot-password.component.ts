import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ForgetPassword } from 'src/app/Models/forget-password.model';
import { ForgetPasswordService } from 'src/app/Services/forget-password.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css'],
})
export class ForgotPasswordComponent implements OnDestroy {
  forgetPasswordFormGroup = new FormGroup({
    UserEmailAddress: new FormControl('', [
      Validators.minLength(3),
      Validators.required,
      Validators.email,
    ]),
  });
  serviceSubscription: Subscription = new Subscription();
  public serverMss:string="";
  
  get inValidEmail() {
    return this.forgetPasswordFormGroup.controls.UserEmailAddress.invalid && (this.forgetPasswordFormGroup.controls.UserEmailAddress.dirty || this.forgetPasswordFormGroup.controls.UserEmailAddress.touched);
  }
  get foundErrorMessage()
  {
    return this.serverMss
  }
  constructor(private forgetPasswordServices: ForgetPasswordService) {}
  SendEmail() {
    if (this.forgetPasswordFormGroup.valid) {
      let forgetPassword = new ForgetPassword(
        this.forgetPasswordFormGroup.controls['UserEmailAddress'].value
      );
      console.log(forgetPassword.userEmailAddress);
      this.serviceSubscription = this.forgetPasswordServices
        .SendEmailToResetPassword(forgetPassword)
        .subscribe({
          next: (data) => {
            console.log('Email sent successfully ' + forgetPassword.userEmailAddress);
          },
          error: (err:HttpErrorResponse) => {
            console.log('error: ' + err.error);
            this.serverMss =err.error;
          },
        });
    }
  }
  ngOnDestroy(): void {
    this.serviceSubscription.unsubscribe();
  }
}
