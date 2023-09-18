import { Component, OnInit } from '@angular/core';
import { AppOwnerServiceService } from 'src/app/Services/app-owner-service.service';
import { MenuItemService } from 'src/app/Services/menu-item.service';
import { RestuarantService } from 'src/app/Services/restuarant.service';

@Component({
  selector: 'app-app-owner',
  templateUrl: './app-owner.component.html',
  styleUrls: ['./app-owner.component.css']
})
export class AppOwnerComponent implements OnInit  {
constructor( private myservice : AppOwnerServiceService ,private menuitemService :MenuItemService,private RestaurantService:RestuarantService){}

allresAdmins:any;
allMenauitems:any;
allresAdmin:any;
allRestaurant:any;
ngOnInit(): void {
  this.ResRequest();
}

  LoadMenuItemRequests(){
    this.menuitemService.GetAllMenuItemAppOwner().subscribe({
      next:(data:any)=>{this.allMenauitems=data;  
      console.log(data)
      },
       error:(err:any)=>{console.log(err.error)}
  })
  }
  LoadResAdminRequests(){
    this.myservice.GetAllResAdmins().subscribe({
      next:(data)=>{
         this.allresAdmins = data;
          console.log(this.allresAdmins)
      }
    });
  }
  Delete(id:any,row:any)
  {
    this.myservice.deleteResAdmin(id).subscribe({
      next:(data)=>{
        row.remove();
      }
    })
  }
  DeleteMenuItem(id:string, row:any){
    this.menuitemService.Delete(id).subscribe({
      next:(data)=>{
        row.remove();
      },
      error:(err)=>{}
  });
}
ResRequest(){
  this.RestaurantService.RestaurantRequests().subscribe({
    next:(data:any)=>{
      this.allRestaurant=data;
      console.log(data);
    }
  })
}
DeleteRestaurant(id:any, row:any){
  this.RestaurantService.DeleteRestaurant(id).subscribe({
    next:(data)=>{
      row.remove();
    },
    error:(err)=>{}
});
}}


