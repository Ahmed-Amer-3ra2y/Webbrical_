import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ResetPassword } from 'src/app/Models/reset-password.model';
import { ForgetPasswordService } from 'src/app/Services/forget-password.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-setnewpassword',
  templateUrl: './setnewpassword.component.html',
  styleUrls: ['./setnewpassword.component.css'],
})
export class SetnewpasswordComponent implements OnInit {
  userid : string = '';
  token: string = '';
  successMassege: string = '';
  errorMassege: string = '';
  newPassword = new FormControl('', [
    Validators.required,
    Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[_.@$!%*?&])[A-Za-z\d_.@$!%*?&]{8,}$/),
  ]);
  confirmPassword = new FormControl('', [Validators.required]);

  get passwordMatchValidator() {
    let password = this.newPassword.value;
    let confirmPassword = this.confirmPassword.value;
    return password !== confirmPassword;
  }
  constructor(
    private forgetService: ForgetPasswordService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.userid  = params['userid'];
      
      this.token = params['token'];
    });
  }
  ResetPassword() {
    if (
      this.newPassword.valid &&
      this.confirmPassword.valid &&
      !this.passwordMatchValidator
    ) {
      let model: ResetPassword = new ResetPassword(
        this.userid ,
        this.newPassword.value,
        this.confirmPassword.value,
        this.token
      );
      this.forgetService.ResetPassword(model).subscribe({
        next: (data) => {
          this.successMassege = 'Password Reset successfully';
          this.router.navigateByUrl('/login');
        },
        error: (err: HttpErrorResponse) => {
          this.errorMassege = err.error;
        },
      });
    }else{
    this.errorMassege = 'not valid inputs';
    }
  }
}
