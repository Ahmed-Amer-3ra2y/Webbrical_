import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RestuarantService } from 'src/app/Services/restuarant.service';

@Component({
  selector: 'app-accept-restaurant',
  templateUrl: './accept-restaurant.component.html',
  styleUrls: ['./accept-restaurant.component.css']
})
export class AcceptRestaurantComponent implements OnInit{
  ID:any;
  ResID:any;
  resAdminID:any;
  ResData:any;
  updateposterFile:any;
  updatebannerFile :any;
  constructor( public route: Router,myRoute:ActivatedRoute,private restuarantService:RestuarantService){
    this.ID = myRoute.snapshot.params["id"];
  }
  ngOnInit(): void {
   this.GetResByID(this.ID);
  }
  header = new HttpHeaders({
    'Content-Type': 'multipart/form-data',
    'Accept': 'application/json'
   });
  EditRestaurantValidations = new FormGroup({
    Location :new FormControl(null,[Validators.required]),
    Name :new FormControl(null,[Validators.required,Validators.maxLength(50)]),
    Description :new FormControl(null,[Validators.required,]),
    EmailAddress :new FormControl(null,[Validators.required,Validators.email]),
    phoneNum:new FormControl(null,[Validators.required,Validators.maxLength(11)]),
    PosterFile:new FormControl(null,[Validators.required]),
    BannearFile:new FormControl(null,[Validators.required]),
    })

    
  updateRes(location:any,name:any,desc:any,emailadd:any,phonee:any,IsAccept:any){
    
      const formData = new FormData();
      formData.append("RestaurantID",this.ID);
      formData.append("Location",location);
      formData.append("Name",name);
      formData.append('Description',desc);
      formData.append('EmailAddress',emailadd);
      formData.append('phoneNum',phonee);
      console.log(this.ResData.resAdminID);
      formData.append('ResAdminID', this.ResData.resAdminID);
      console.log("banner");
      console.log(this.ResData.coverBanner);
      console.log(this.ResData.poster);
      formData.append('IsAccept',IsAccept);
      formData.append('Poster',this.ResData.poster);
      formData.append('CoverBanner',this.ResData.coverBanner);
      //formData.append('BannearFile',this.ResData.coverBanner);
     
     
     console.log(emailadd);
     console.log(phonee);
     this.restuarantService.UpdateRestuarant(formData,this.header,+this.ID).subscribe( {

      next:()=>{
   
         // Hide the message after 30 seconds
        this.route.navigateByUrl("/appOwner");



 }});


 
}

    GetResByID(id:any){
      this.restuarantService.GetRestuarantById(id).subscribe({
        next:(data:any)=>{this.ResData = data;
          console.log("edit");
          console.log(data);
          this.EditRestaurantValidations.patchValue({
            Location : this.ResData.location,
            Name : this.ResData.name,
            Description: this.ResData.description,
            EmailAddress: this.ResData.emailAddress,
            phoneNum: this.ResData.phoneNum,
           // PosterFile: this.ResData.posterFile,
           // BannearFile:this.ResData.bannerFile
          });
        }
      })
    }
    private validateAllFormFields(formGroup: FormGroup){
      Object.keys(formGroup.controls).forEach(field =>{
        const control = formGroup.get(field);
        if(control instanceof FormControl){
          control.markAsDirty({onlySelf: true});
        }else if ( control instanceof FormGroup){
          this.validateAllFormFields(control);
        }
      })
    }
    UpdatePoster(event:any){
      this.updateposterFile = event.target.files[0];
      console.log(this.updateposterFile.name)
    }
    UpdateBanner(event:any){
      this.updatebannerFile = event.target.files[0];
      console.log(this.updatebannerFile .name)
    }
}
