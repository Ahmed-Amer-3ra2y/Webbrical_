import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Restaurant } from '../Models/restaurant.model';

@Injectable({
  providedIn: 'root'
})
export class RestuarantService {
 private BaseURL:string = "https://localhost:44318/Restaurants"
  constructor(private httpClient:HttpClient) { }
  AddRestaurant(formData:FormData,header:any)
  {
    return this.httpClient.post(this.BaseURL,formData,header);
  }

  GetAllRestuarants(){
    return this.httpClient.get(this.BaseURL);
  }

  GetRestuarantById(id:any){
    return this.httpClient.get(`${this.BaseURL}/${id}`);

  }
  GetRestuarantB(name:any){
    return this.httpClient.get(`${this.BaseURL}/${name}`);

  }
  GetRestaurantByResAdminID(resAdminID:string)
  {
    return this.httpClient.get(`${this.BaseURL}/ResExist/${resAdminID}`);
  }

UpdateRestuarant(Res:any,header:any,id:any){
    return this.httpClient.put(this.BaseURL+"/"+id,Res,header);
  }
  RestaurantRequests(){
    return this.httpClient.get(this.BaseURL+"/appOwner");
  }
  DeleteRestaurant(id:any){
    return this.httpClient.delete(this.BaseURL+"/"+id);
  }
}
