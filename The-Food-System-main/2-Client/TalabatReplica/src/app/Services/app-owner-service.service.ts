import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppOwnerServiceService {
  private BaseUrl = "https://localhost:44318/api/resAdmin";


  constructor(private http:HttpClient) { }

  GetAllResAdmins(){
    return this.http.get(this.BaseUrl);
  }
deleteResAdmin(id:any)
{
  return this.http.delete(`${this.BaseUrl}/${id}`);
}
GetRestuarantAdminById(id:any){
  return this.http.get(`${this.BaseUrl}/${id}`);

}

updateRestAdmin(id:any,resAdmin:any)
{
  return this.http.put(`${this.BaseUrl}/${id}`,resAdmin);
}

GetResIDByUserName(userName:string){
  return this.http.get(`${this.BaseUrl}/${userName}`);
}

GetAdminIDByUserName(userName:string){
  return this.http.get(`${this.BaseUrl}/Admin/${userName}`);
}

}
