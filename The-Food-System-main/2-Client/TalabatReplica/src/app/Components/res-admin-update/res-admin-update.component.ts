import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppOwnerServiceService } from 'src/app/Services/app-owner-service.service';

@Component({
  selector: 'app-res-admin-update',
  templateUrl: './res-admin-update.component.html',
  styleUrls: ['./res-admin-update.component.css']
})
export class ResAdminUpdateComponent implements OnInit {

  ngOnInit(): void {
this.myservice.GetRestuarantAdminById(this.id).subscribe({
  next:(data)=>{
    this.itemdetails=data;
    console.log(data)
    console.log(this.itemdetails)

//bind data comming from api to each formControl of form group in html ya Menna : Peter

    this.myValidations.patchValue({
      email: this.itemdetails.email,
      id: this.itemdetails.id,
      firstName:this.itemdetails.firstName,
      lastName: this.itemdetails.lastName,
      emailConfirm: this.itemdetails.emailConfirm
    })

   },
   error:(error)=>{console.log(error)}
});


  }
  id:any;
  constructor(private myRoute:ActivatedRoute,private myservice:AppOwnerServiceService,private router: Router){
this.id=myRoute.snapshot.params["id"];
console.log(this.id);
  }
  itemdetails:any;

  myValidations:FormGroup=new FormGroup({
    email :new FormControl(null,[Validators.required,Validators.email]),
    firstName :new FormControl(null,[Validators.required]),
    lastName :new FormControl(null,[Validators.required]),
    emailConfirm :new FormControl(null),
    id:new FormControl(null)
  });


  updateItem()
  {
    //if form valid
    if(this.myValidations.valid)
    {
// init a new object
      let res={
        email:this.myValidations.controls["email"].value,
        firstName:this.myValidations.controls["firstName"].value,

        lastName:this.myValidations.controls["lastName"].value,
        id:this.myValidations.controls["id"].value,
        userName :this.itemdetails.userName,
        emailConfirm:true


      }


      console.log(res);
      // call my service4   
this.myservice.updateRestAdmin(this.id,res).subscribe({
  next:(result)=>{
    this.router.navigateByUrl('/appOwner');
  }
});
    }
  }

}
