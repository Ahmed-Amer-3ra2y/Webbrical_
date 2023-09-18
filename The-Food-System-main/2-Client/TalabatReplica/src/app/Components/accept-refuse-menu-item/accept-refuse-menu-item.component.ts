import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { MenuItemService } from 'src/app/Services/menu-item.service';

@Component({
  selector: 'app-accept-refuse-menu-item',
  templateUrl: './accept-refuse-menu-item.component.html',
  styleUrls: ['./accept-refuse-menu-item.component.css']
})
export class AcceptRefuseMenuItemComponent implements OnInit {
  id:any;
  menuItem:any;
constructor(private menuItemServices: MenuItemService, private rout:ActivatedRoute, private route: Router)
{
  this.id=rout.snapshot.params["id"];
  console.log(this.id)
}



  ngOnInit(): void {

    this.menuItemServices.GetItemById(this.id).subscribe({
      next:(data:any)=>{this.menuItem=data ;
      console.log("data",this.menuItem)
      console.log("ffff",this.menuItem.offer,this.menuItem.isTopItem);
      this.myValidations.patchValue({
        name:this.menuItem.name ,
        itItem:this.menuItem.itItem,
        description:this.menuItem.description,
        price:this.menuItem.price,
        size:this.menuItem.size,
        isAccepted:this.menuItem.isAccepted,
        resturantID:this.menuItem.resturantID,
        cName:this.menuItem.cName,
        offer:this.menuItem.offer,
        isTopItem:this.menuItem.isTopItem



      }
      )},
      error:(err)=>{console.log(err.error) }
    })
  }

myValidations:FormGroup=new FormGroup({
 // itemID :new FormControl(null),
  name:new FormControl(null),
  price:new FormControl(null),
  description:new FormControl(null),
  size:new FormControl(null),
  isAccepted:new FormControl(null),
  resturantID:new FormControl(null),
  offer:new FormControl(null),
  CategoryID:new FormControl(null),
  cName:new FormControl(null),
  isTopItem:new FormControl(null)

})




 //   formData.append('PhotoFile',this.file);



  



/* update()
{

      const formData = new FormData();
formData.append('itemID',this.menuItem.itemID);
      formData.append('Name',this.menuItem.name);
      formData.append('price',this.menuItem.price);
      formData.append('Description',"Edit");
      formData.append('size',this.menuItem.size);

      formData.append('IsTopItem',this.menuItem.IsTopItem);
      formData.append('ResturantID',this.menuItem.resturantID);
      formData.append('Image',this.menuItem.Image);

      formData.append('isAccepted',this.menuItem.isAccepted);
      formData.append("offer",this.menuItem.offer);
      formData.append("categoryID",this.menuItem.categoryID);
      formData.append("cName",this.menuItem.cName);
      formData.append("photoFile",this.menuItem.photoFile);




      this.menuItemServices.updateItem(formData,this.id,this.header) */

      selectedFile:any;

      SelectedFile(event: any) {
        this.selectedFile = event.target.files[0];
        console.log(this.selectedFile.name)

      }
      updateItem(itemID:any,name:any,price:any,description:any,size:any,IsTop:any,resturantID:any,offer:any,categoryID:any,sAccepted:any){


        const formData = new FormData();
        formData.append('ItemID',itemID);
        formData.append('Name',name);
        formData.append('price',price);
        formData.append('Description',description);
        formData.append('size',size);
        formData.append('ResturantID',resturantID);
        formData.append('CategoryID',categoryID);
        formData.append('ResturantID',resturantID);
        formData.append('image',this.menuItem.image);

        //if(IsTop == 'on'){
          formData.append('IsTopItem',IsTop);
      //  }
       // else
       // {
          //formData.append('IsTopItem','false');
       // }

       // if( offer == 'on' ){
          formData.append('Offer',offer);

       // }else
       // {
       //   formData.append('offer',"false");
       // }



          formData.append('IsAccepted','true');



     //   formData.append('',sAccepted)

       formData.append('PhotoFile',this.menuItem.photoFile);

         const header = new HttpHeaders({
          'Content-Type': 'multipart/form-data',
          'Accept': 'application/json'
         });
        //console.log({itemID,name,price,description,size,resturantID,categoryID},+this.id);
        this.menuItemServices.updateItem(formData,this.id,header).subscribe({
          next:()=>{
            alert('product is accepted')
            this.route.navigateByUrl("/appOwner");
          }
        });
       console.log(sAccepted);
        }


      }







     /*  if(offer == 'on'){
        formData.append('offer','true');

      } */


     // formData.append('PhotoFile',this.file);

  







