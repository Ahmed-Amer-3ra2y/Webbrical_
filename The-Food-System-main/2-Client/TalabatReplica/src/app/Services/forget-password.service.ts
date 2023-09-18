import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResetPassword } from '../Models/reset-password.model';
import { ForgetPassword } from '../Models/forget-password.model';

@Injectable({
  providedIn: 'root'
})
export class ForgetPasswordService {
private BaseUrl:string="https://localhost:44318/ForgetPassword"
  constructor(private httpClient:HttpClient) { }
  
  SendEmailToResetPassword(forgetPassword:ForgetPassword){
    return this.httpClient.post(this.BaseUrl,forgetPassword);
  }
  
  ResetPassword(resetPassword:ResetPassword)
  {
    return this.httpClient.put(this.BaseUrl,resetPassword);
  }



}
