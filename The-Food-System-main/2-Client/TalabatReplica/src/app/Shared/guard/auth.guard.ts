import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthServicesService } from 'src/app/Services/auth-services.service';

export const authGuard : CanActivateFn = 
    (next : ActivatedRouteSnapshot, state : RouterStateSnapshot) : boolean | Promise<boolean> |Observable<boolean>=> 
      {
    
        const router : Router=inject(Router)
        const auth : AuthServicesService =inject(AuthServicesService)

    if(auth.isLoggedIn!=true)
    {
      window.alert("Not Authinticated ... ")
      router.navigate(['/login']);
      return false;
    }
   return true;
      
      
  };
  

