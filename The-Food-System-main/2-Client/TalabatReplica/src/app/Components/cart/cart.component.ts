import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  ngOnInit(): void {
    this.gitcartitems()
  }
  dishesInCart:any[] =[]
  quantaty: any
total:any=0;
Shipping:any=0;

  quantatychange(){

    localStorage.setItem("cart",JSON.stringify(this.dishesInCart))
    console.log(this.quantaty)
    this.gitCartTotalPrice()
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

gitCartTotalPrice(){
this.total=0;
for(let x in this.dishesInCart){
  this.total += this.dishesInCart[x].price *  this.dishesInCart[x].quantity
  console.log(this.total)
}
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

deleteItem(indx:any){
  this.dishesInCart.splice(this.dishesInCart.find(item => item.id == indx.id),1)
  localStorage.setItem("cart",JSON.stringify(this.dishesInCart))
  this.gitCartTotalPrice()
}
deleteAll(){
  this.dishesInCart.splice(0,)
  this.total=0;
  this.Shipping=0;
  localStorage.setItem("cart",JSON.stringify(this.dishesInCart))

}
}
