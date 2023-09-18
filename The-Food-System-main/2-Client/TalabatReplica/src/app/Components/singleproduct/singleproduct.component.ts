import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuItemService } from 'src/app/Services/menu-item.service';

@Component({
  selector: 'app-singleproduct',
  templateUrl: './singleproduct.component.html',
  styleUrls: ['./singleproduct.component.css']
})
export class SingleproductComponent  implements OnInit {
  item:any;
  id:any;
  constructor(public service:MenuItemService,Route:ActivatedRoute){
    this.id = Route.snapshot.params['itemID'];
  }
  ngOnInit(): void {
      this.GetItemById(this.id);
  }
 GetItemById(id:any){
  this.service.GetItemById(id).subscribe({
    next:(data)=>{this.item=data;console.log(data);}
  });
 }
}
