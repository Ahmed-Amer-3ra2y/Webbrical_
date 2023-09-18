import { Restaurant } from './../../Models/restaurant.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from 'src/app/Services/category.service';
import { SideCartService } from'src/app/Services/side-cart.service';
import { MenuItemService } from 'src/app/Services/menu-item.service';
import { RestuarantService } from 'src/app/Services/restuarant.service';
import { from } from 'rxjs';

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.css']
})
export class RestaurantComponent implements OnInit {
  name:any;
  categories:any;
  items:any;
  alldishes:any;
  topdishes:any;
  Offer:any;
  dishesInCart:any[] = []
  quantity:number = 1 ;
  amountInput:any="#editEmployeeModal";
  ResID:any;
  Restaurant:any;
      constructor(  private castservice:SideCartService , public  service:CategoryService,public MenuService :MenuItemService  , myRoute: ActivatedRoute , public ResService:RestuarantService){
               this.ResID = myRoute.snapshot.params["restaurantID"];
      }
  ngOnInit(): void {


    this.MenuService.GetAllCategoriesByResID(this.ResID).subscribe({
      next:(data)=>{
        this.categories = data;
      }
    })

    /*this.service.GetAllDises().subscribe({
      next:(data:any)=>{
         this.alldishes = data;
        //  console.log(this.alldishes)
      }
    })*/

    this.GetResByID(this.ResID);
    this.GetMenuItemByResID(this.ResID);
    this.GetTopdishes(this.ResID);
    this.GetOffer(this.ResID);

  }

  GetResByID(id:any){
    this.ResService.GetRestuarantById(id).subscribe({
      next:(data)=>{this.Restaurant = data;}
    })
  }



  filterbycat(catname:any){
   let value  = catname.target.value
  /* if (value == "All" ){
    this.GetMenuItemByResID(this.ResID);
 }
 this.alldishes = this.alldishes.filter((item: { categoryName: string }) => {
   return item.categoryName.toLowerCase().includes(value.toLowerCase())
 });*/
if(value=="All"){
 this.GetMenuItemByResID(this.ResID)

}else
    this.GetCategoryByName(value);



  }

  /////////////////////////////////////////// Add Dishes TO Cart
  amount(amount:any){
   this.quantity = amount.target.value;
  }
 save(){
   console.log(this.quantity)
  }
addtocart(data:any){
  if("cart" in localStorage){
  data.quantity = this.quantity ;
  console.log(data)
  this.dishesInCart= JSON.parse(localStorage.getItem("cart")!)
  let exist =
  this.dishesInCart.find(item => item.itemID
    == data.itemID)
  if(exist){
    alert("product is already in your cart")
    console.log(data.itemID)
  }else{
    // this.castservice.gitcartitems()
    // this.castservice.total
    this.castservice.dishesInCart.push(data)
    // this.castservice.gitCartTotalPrice()

    // this.castservice.gitCartTotalPrice()
    // this.castservice.quantatychange()
    alert(" Don ")
    this.dishesInCart.push(data)
    localStorage.setItem("cart",JSON.stringify(this.dishesInCart))
  }
}else{
  alert(" Don ")
  this.castservice.dishesInCart.push(data)
  // this.castservice.gitCartTotalPrice()
  data.quantity = this.quantity ;
  console.log(data)
  this.dishesInCart.push(data)
  localStorage.setItem("cart",JSON.stringify(this.dishesInCart))
}
  }
  GetMenuItemByResID(ResID:any){
    this.MenuService.GetMenuItemByResID(ResID).subscribe({
      next:(data)=>{this.alldishes= data;console.log(data);}
    })
  }

GetCategoryByName(name:any){

  this.service.GetCategoryByName(name).subscribe({
    next:(data:any)=>{
      for( let key in data) {
        this.alldishes = data[key]["menuItems"];
        console.log("dishes2");
        console.log( this.alldishes);
       /* for (let i = 0; i < this.alldishes2.length; i++) {
          this.alldishes=[this.alldishes2[i]];
          console.log( "dishes"+this.alldishes);
        }*/
      }

      /*for (let key in data) {
        this.alldishes=data[key]["menuItems"];

        console.log("data"+data[key][menuItems]);
      }
*/
    }
  });
}
GetTopdishes(ResID:any){
  this.MenuService.GetTopdishes(ResID).subscribe({
    next:(data:any)=>{
      this.topdishes=data;
      console.log("*******")
      console.log(data);
    }
  })
}
GetOffer(ResID:any){
  this.MenuService.GetOffer(ResID).subscribe({
    next:(data:any)=>{
      this.Offer=data;
      console.log("offer");
      console.log(data);
    }
  })
}


}
