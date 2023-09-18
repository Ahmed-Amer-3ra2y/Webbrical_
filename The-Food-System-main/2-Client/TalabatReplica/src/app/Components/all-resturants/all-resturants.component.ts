import { Component, OnInit } from '@angular/core';
import { RestuarantService } from 'src/app/Services/restuarant.service';

@Component({
  selector: 'app-all-resturants',
  templateUrl: './all-resturants.component.html',
  styleUrls: ['./all-resturants.component.css']
})
export class AllResturantsComponent implements OnInit {
  constructor(public service : RestuarantService){}
  Restarunts :any ;
  ResName:any;
  ResBySeach:any;
  item:any;
  SearchTerm:any;
  oneRes:any;
  ngOnInit(): void {
       this.GetAllRestuarants();
  }
GetAllRestuarants(){
  this.service.GetAllRestuarants().subscribe({
    next:(data)=>{this.Restarunts=data;console.log(data);
    // for(let item in this.Restarunts){
    //   console.log(this.Restarunts[item]);
    //   this.oneRes = this.Restarunts[item]
    // }
    },
    error:(error)=>{console.log(error);}
  });

}
  // ResSearch(ResName:any){


  // }

  GetResByName(name:string){
    if (name == null ){
      this.GetAllRestuarants();
    }
    this.service.GetRestuarantB(name).subscribe({
      next:(data)=>{
        var res =[data]
        this.Restarunts = res;console.log("ddddd=",data)}
    })
  }
  SearchRes(searchTerm: string) {
    if (searchTerm == "" ){
       this.GetAllRestuarants();
    }
    this.Restarunts = this.Restarunts.filter((item: { name: string }) => {
      return item.name.toLowerCase().includes(searchTerm.toLowerCase())
    });
  }

}
