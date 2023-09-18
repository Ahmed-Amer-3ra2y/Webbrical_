import { AppOwnerServiceService } from 'src/app/Services/app-owner-service.service';
import { HttpErrorResponse, HttpHeaders, HttpStatusCode } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Restaurant } from 'src/app/Models/restaurant.model';
import { CategoryService } from 'src/app/Services/category.service';
import { MenuItemService } from 'src/app/Services/menu-item.service';
import { RestuarantService } from 'src/app/Services/restuarant.service';

@Component({
  selector: 'app-admin-dashbord-menue',
  templateUrl: './admin-dashbord-menue.component.html',
  styleUrls: ['./admin-dashbord-menue.component.css']
})
export class AdminDashbordMenueComponent {
  items:any;
  Category:any;
  cate:any;
  ID:any;
  itemdetails:any;
  file:any;
  username=JSON.parse(localStorage.getItem("user")!).userName;
  ResID:any;
  ResName:any;
  ResPoster:any;
  menuSubmitted:boolean=false;
  menuupdated:boolean=false;
  myValidations!: FormGroup;
  addRestaurantValidations!: FormGroup;
  //EditRestaurantValidations!:FormGroup;
  resAdminID:any;
  addresForm:boolean=false;
  editResForm:boolean=true;
  resExist:boolean=false;
  ResData:any;
  updateposterFile:any;
  updatebannerFile :any;
  constructor(public appService:AppOwnerServiceService ,private fb : FormBuilder,public route: Router,public service:MenuItemService,public CategorieService:CategoryService,myRoute:ActivatedRoute,private restuarantService:RestuarantService){
    this.ID = myRoute.snapshot.params["itemID"];
  }
  header = new HttpHeaders({
    'Content-Type': 'multipart/form-data',
    'Accept': 'application/json'
   });
  ngOnInit(): void {
    this.restuarantService.GetRestaurantByResAdminID(this.resAdminID).subscribe({
      next:()=>{this.resExist=true},
      error:()=>{this.resExist=false;}

    })
// this.GetAllMenuItems()
    this.GetItemByID(this.ID);
    this.GetAllCategories();
    this.GetResIDByUserName(this.username);
    this.GetAdminIDByUserName(this.username);


    this.myValidations = this.fb.group({
      title:['',[Validators.maxLength(50),Validators.required,Validators.pattern(/^[a-zA-Z\s]*$/)]],
      price:['',[Validators.required,Validators.pattern(/^[0-9]+$/)]],
      size:['',[Validators.required,Validators.pattern(/^[MmLlSs]+$/)]],
      desc:['',[Validators.required]],
      img:['',[Validators.required]],
    });

    this.addRestaurantValidations = this.fb.group({
      Location:['',[Validators.required]],
      Name:['',[Validators.required,Validators.maxLength(50)]],
      Description:['',[Validators.required,]],
      EmailAddress:['',[Validators.required,Validators.email]],
      phoneNum:['',[Validators.required,Validators.maxLength(11)]],
      //ResAdminID:['',[Validators.required]],
      PosterFile:[null,Validators.required],
      BannearFile:[null,Validators.required]
    })




  }
  EditRestaurantValidations = new FormGroup({
    Location :new FormControl(null,[Validators.required]),
    Name :new FormControl(null,[Validators.required,Validators.maxLength(50)]),
    Description :new FormControl(null,[Validators.required,]),
    EmailAddress :new FormControl(null,[Validators.required,Validators.email]),
    phoneNum:new FormControl(null,[Validators.required,Validators.maxLength(11)]),
    PosterFile:new FormControl(null,[Validators.required]),
    BannearFile:new FormControl(null,[Validators.required]),
    })

  GetMenuItemByResID(ResID:any){
    this.service.GetMenuItemByResID(ResID).subscribe({
      next:(data)=>{this.items= data;
      console.log("data");
      console.log(data);
      console.log(this.ResID);}
    })
    if(this.ResID != null){
      this.addresForm = true;
      this.editResForm = false;
    }
    this.GetResByID(this.ResID);

  }

  updateRes(location:any,name:any,desc:any,emailadd:any,phonee:any){
    if(this.EditRestaurantValidations.valid){
      const formData = new FormData();
      formData.append("RestaurantID",this.ResID);
      formData.append("Location",location);
      formData.append("Name",name);
      formData.append('Description',desc);
      formData.append('EmailAddress',emailadd);
      formData.append('phoneNum',phonee);
      formData.append('ResAdminID', this.resAdminID);
     formData.append('BannearFile', this.updatebannerFile );
     formData.append('PosterFile',this.updateposterFile);
     formData.append('IsAccept','false');
     console.log(emailadd);
     console.log(phonee);
     this.restuarantService.UpdateRestuarant(formData,this.header,+this.ResID).subscribe( {

      next:()=>{
        this.menuupdated= true;
        setTimeout(() => {
          this.menuupdated  = false;
        }, 3000); // Hide the message after 30 seconds
        this.route.navigateByUrl("/Adminmenu");



 }});


  }
  else{
    console.log("error")
    this.validateAllFormFields(this.EditRestaurantValidations);
  }
}

  Additem (name:any,price:any,description:any,size:any,IsTop:any,resturantID:any,offer:any,categoryID:any){
    console.log("kkkk===>",IsTop,offer)

    if(this.myValidations.valid){
      const formData = new FormData();
      formData.append('Name',name);
      formData.append('price',price);
      formData.append('Description',description);
      formData.append('size',size);
      formData.append('IsAccepted','false');
      //if(IsTop == 'on'){
        formData.append('IsTopItem',IsTop);

      //}
     // else{
        //formData.append('IsTopItem','false');

      //}
      formData.append('ResturantID',this.ResID);
      //if(offer == 'on'){
        formData.append('Offer',offer);

      //}
     // else{
       // formData.append('Offer','false');
     // }



      formData.append('CategoryID',categoryID);
      formData.append('PhotoFile',this.file);


//console.log({IsTop},{offer});
    this.service.Additem(formData,this.header).subscribe( {next:()=>{this.GetMenuItemByResID(this.ResID);}} )

    this.route.navigateByUrl("/Adminmenu");
    this.myValidations.reset();
    this.menuSubmitted = true;
    setTimeout(() => {
      this.menuSubmitted  = false;
    }, 3000);

    }else{
      this.validateAllFormFields(this.myValidations);
    }
  }
  restaurantModel: Restaurant=new Restaurant();

  AddRestaurant (){
    console.log(this.restaurantModel)
    this.restaurantModel.ResAdminID=this.resAdminID;
    if(this.addRestaurantValidations.valid){
      const formData = new FormData();
      formData.append('Name',this.restaurantModel.Name);
      formData.append('Location',this.restaurantModel.Location);
      formData.append('EmailAddress',this.restaurantModel.EmailAddress);
      formData.append('phoneNum',this.restaurantModel.phoneNum);
      formData.append('Description',this.restaurantModel.Description);
      console.log(this.restaurantModel.ResAdminID);
      formData.append('ResAdminID',this.restaurantModel.ResAdminID);
     formData.append('BannearFile', this.restaurantModel.BannearFile);
     formData.append('PosterFile', this.restaurantModel.PosterFile);
     formData.append('IsAccept','false');
   this.restuarantService.AddRestaurant(formData,this.header).subscribe( {
    next:()=>{this.menuSubmitted = true;
      setTimeout(() => {
        this.menuSubmitted  = false;
      }, 3000);},
    error:(err:HttpErrorResponse)=>{console.log(err.error)},
    complete:()=>{}
   } )
   //this.route.navigateByUrl("/Adminmenu");
   this.addRestaurantValidations.reset();
  // this.route.navigateByUrl("/Adminmenu");
    }else{
      console.log("error")
      this.validateAllFormFields(this.addRestaurantValidations);
    }
  }
  get NameValid(){
    return this.myValidations.controls["title"].valid;
  }

  get NameExist(){
    return this.myValidations.controls["title"].value;
  }


  GetItemByID(id:any){
    this.service.GetItemById(id).subscribe({
      next:(data)=>{this.itemdetails=data},
      error:(err)=>{console.log(err)}

    })

  }
  Delete(id:any,row:any){
    this.service.Delete(id).subscribe();
    row.remove();
  }
  GetAllCategories(){
    this.CategorieService.GetAllCategories().subscribe({
      next:(data)=>{this.Category=data;console.log(data);},
      error:(err)=>{console.log(err)}
    })
  }

  UploadFile(event:any){
    this.file = event.target.files[0];
    console.log(this.file);
  }

  UploadPoster(event:any)
  {
    this.restaurantModel.PosterFile=event.target.files[0];
    console.log(this.restaurantModel.PosterFile);

  }
  UploadBanner(event:any){
    this.restaurantModel.BannearFile=event.target.files[0];
    console.log(this.restaurantModel.BannearFile);
  }
  UpdatePoster(event:any){
    this.updateposterFile = event.target.files[0];
    console.log(this.updateposterFile.name)
  }
  UpdateBanner(event:any){
    this.updatebannerFile = event.target.files[0];
    console.log(this.updatebannerFile .name)
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

  GetResIDByUserName(userName:string){
    this.appService.GetResIDByUserName(userName).subscribe({
      next:(data:any)=>{this.ResID =data[0]['restaurantID'] ;
      this.ResName =data[0]['name'] ;
      this.ResPoster = data[0]['poster'] ;
      console.log(data[0]['restaurantID']);
      this.GetMenuItemByResID(this.ResID);}})
  }
  //Add Restaurant
  GetAdminIDByUserName(userName:string){
    this.appService.GetAdminIDByUserName(userName).subscribe({
      next:(data:any)=>{this.resAdminID =data[0]['id'] ;
      console.log(data[0]['id']);
      }
  });
  }

  GetResByID(id:any){
    this.restuarantService.GetRestuarantById(id).subscribe({
      next:(data:any)=>{this.ResData = data;
        this.EditRestaurantValidations.patchValue({
          Location : this.ResData.location,
          Name : this.ResData.name,
          Description: this.ResData.description,
          EmailAddress: this.ResData.emailAddress,
          phoneNum: this.ResData.phoneNum,
          PosterFile: this.ResData.posterFile,
          BannearFile:this.ResData.bannerFile
        });
      }
    })
  }
}
