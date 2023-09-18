import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RestaurantComponent } from './Components/restaurant/restaurant.component';
import { LoginComponent } from './Components/login/login.component';
import { ErrorComponent } from './Components/error/error.component';
import { CheckOutComponent } from './Components/check-out/check-out.component';
import { AllResturantsComponent } from './Components/all-resturants/all-resturants.component';
import { SingleproductComponent } from './Components/singleproduct/singleproduct.component';
import { CartComponent } from './Components/cart/cart.component';
import { PaymentComponent } from './Components/payment/payment.component';
import { ForgotPasswordComponent } from './Components/forgot-password/forgot-password.component';
import { SetnewpasswordComponent } from './Components/setnewpassword/setnewpassword.component';
import { ContactComponent } from './Components/contact/contact.component';
import { CategoryItemsComponent } from './Components/category-items/category-items.component';
import { AdminDashbordMenueComponent } from './Components/admin-dashbord-menue/admin-dashbord-menue.component';
import { FilterbycatComponent } from './Components/filterbycat/filterbycat.component';
import { UpdatemenuComponent } from './Components/updatemenu/updatemenu.component';
import { authGuard } from './Shared/guard/auth.guard'
import { AppOwnerComponent } from './Components/app-owner/app-owner.component';
import { ResAdminUpdateComponent } from './Components/res-admin-update/res-admin-update.component';
import { roleGuard } from './Shared/guard/role.guard';
import { appOwnerGuard } from './Shared/guard/app-owner.guard';
import { AcceptRefuseMenuItemComponent } from './Components/accept-refuse-menu-item/accept-refuse-menu-item.component';
import { AcceptRestaurantComponent } from './Components/accept-restaurant/accept-restaurant.component';


const routes: Routes = [
  {path:"", component: AllResturantsComponent},
  {path:"ResProfile/:restaurantID", component: RestaurantComponent},
  {path:"category/:name", component: CategoryItemsComponent},
  {path:"login", component: LoginComponent},
  {path:"checkOut", component: CheckOutComponent},
  {path:"AllResturants", component: AllResturantsComponent},
  {path:"singleProduct/:itemID", component: SingleproductComponent},
  {path:"cart", component: CartComponent},
  {path:"pay", component: PaymentComponent ,canActivate:[authGuard]},
  {path:"Adminmenu", component: AdminDashbordMenueComponent,canActivate:[authGuard, roleGuard] },
  {path:"Updatemenu/:itemID", component: UpdatemenuComponent ,canActivate:[authGuard,roleGuard] },
  {path:"forgetpassword", component: ForgotPasswordComponent },
  {path:"setnewpassword", component: SetnewpasswordComponent },
  {path:"contactus", component: ContactComponent },
  {path:"category", component: CategoryItemsComponent},
  {path:"Filterbycat", component: FilterbycatComponent},
  {path:"appOwner", component: AppOwnerComponent,canActivate:[authGuard,appOwnerGuard]},
  {path:"AcceptMenuItem/:id", component: AcceptRefuseMenuItemComponent,canActivate:[authGuard,appOwnerGuard]},
  {path:"AcceptRes/:id", component: AcceptRestaurantComponent,canActivate:[authGuard,appOwnerGuard]},
  {path:"ResAdminUpdate/:id", component: ResAdminUpdateComponent,canActivate:[authGuard,appOwnerGuard]},
  {path:"**", component: ErrorComponent}


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
