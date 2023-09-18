import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

import { style } from '@angular/animations';
import { PaymentService } from 'src/app/payment.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {




  amount:any;
  @ViewChild('paymentRef', {static: true}) paymentRef! : ElementRef;

constructor(private router: Router, private payment: PaymentService ) { 


}

ngOnInit(): void{
  // this.amount = this.payment['totalAmount'];
  window.paypal.Buttons().render(this.paymentRef.nativeElement)
  console.log(window.paypal)
  }


   cancel() {

    this.router.navigate(['checkOut']);
    
    }
}

