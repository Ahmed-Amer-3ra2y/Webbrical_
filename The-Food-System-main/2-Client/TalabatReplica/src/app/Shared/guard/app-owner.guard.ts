import { Location } from '@angular/common';
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthServicesService } from 'src/app/Services/auth-services.service';

export const appOwnerGuard: CanActivateFn = (route, state) => 
{
  // hint : https://www.youtube.com/watch?v=HnTlsoKaaTk

  const auth : AuthServicesService =inject(AuthServicesService)
  const router : Router=inject(Router)
 const location :Location = inject(Location)
  if(auth.AppOwnerAuthorized!=true)
  {
    alert("don't have privileges to access this page ... ")
    location.back(); // to navigate to previus page instead home
    return false;
  }
 return true;

};
