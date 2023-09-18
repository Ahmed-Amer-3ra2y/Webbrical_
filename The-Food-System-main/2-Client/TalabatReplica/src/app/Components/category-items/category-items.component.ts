import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from 'src/app/Services/category.service';

@Component({
  selector: 'app-category-items',
  templateUrl: './category-items.component.html',
  styleUrls: ['./category-items.component.css']
})
export class CategoryItemsComponent  implements OnInit {
  name:any;
  constructor(public service : CategoryService, myRoute : ActivatedRoute){
    this.name = myRoute.snapshot.params["name"];
    console.log(this.name);


  }
  ngOnInit(): void {
    // this.service.GetCategoryByName(this.name).subscribe({
    //   next:(data)=>{
    //     console.log(data);
    //   }
    // });
  }

}
