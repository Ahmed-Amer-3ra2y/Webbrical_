import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from 'src/app/Services/category.service';

@Component({
  selector: 'app-filterbycat',
  templateUrl: './filterbycat.component.html',
  styleUrls: ['./filterbycat.component.css']
})
export class FilterbycatComponent implements OnInit {
  name:any;
  categories:any;
  items:any;
      constructor(public service:CategoryService, myRoute: ActivatedRoute){
      }
  ngOnInit(): void {


    // this.service.GetAllCategories().subscribe({
    //   next:(data)=>{
    //     this.categories = data;
    //   }
    // });
    // this.service.GetCategoryByName(this.name).subscribe({
    //   next:(data)=>{
    //     console.log(data);
    //   }
    // });

  }

}
