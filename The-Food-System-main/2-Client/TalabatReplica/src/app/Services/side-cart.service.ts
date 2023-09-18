import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SideCartService {

  constructor() { }


  dishesInCart:any[] =[]

  hidde="hidde";
  visabl:any;

  Shipping:any=0;

  quantaty: any
total:any=0;






  gitCartTotalPrice(){
    this.total=0;
    for(let x in this.dishesInCart){
      this.total += this.dishesInCart[x].price *  this.dishesInCart[x].quantity
      console.log(this.total)
    }
    }

    gitcartitems(){
    if("cart" in localStorage){
    this.dishesInCart= JSON.parse(localStorage.getItem("cart")!)
  }
    if(this.dishesInCart.length>0){
      this.Shipping=45
    }
this.gitCartTotalPrice()
}

quantatychange(){

  localStorage.setItem("cart",JSON.stringify(this.dishesInCart))
  console.log(this.quantaty)
  this.gitCartTotalPrice()
}
minsAmount(indx:any){
  indx.quantity --
  this.gitCartTotalPrice()
  localStorage.setItem("cart",JSON.stringify(this.dishesInCart))
}

addAmount(indx:any){
  indx.quantity ++
  this.gitCartTotalPrice()
  localStorage.setItem("cart",JSON.stringify(this.dishesInCart))
}









  hidden(){
    this.hidde="hidde";
  }

  visable(){
    this.hidde="";
  }

i:any =0 ;




}





