import { Injectable, NgZone } from '@angular/core';
import {AngularFireAuth} from '@angular/fire/compat/auth'
import { Login } from 'src/app/Models/login.model'
import {AngularFirestore, AngularFirestoreDocument}  from '@angular/fire/compat/firestore';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AuthServicesService {

  //hint to use firebase :  https://www.positronx.io/full-angular-firebase-authentication-system/
 // create app on console firebase : https://www.youtube.com/watch?v=A-wSkZVxKzU&t=141s
// Generate enviroment.ts file : https://www.youtube.com/watch?v=3IXwN4XJywQ

  userData:any //// Save logged in user data



   //AngularFirestore, // Inject Firestore service to store user data in firebase 
  //AngularFireAuth, // Inject Firebase auth service
 // NgZone service to remove outside scope warning

  constructor(public AFStore: AngularFirestore ,public  AFAuth :AngularFireAuth ,public  router:Router ,public  NgZone:NgZone ) 
  {
     // Saving user data in localstorage when logged in and setting up null when logged out 

     AFAuth.authState.subscribe((user) => {
      if (user) {
        this.userData = user;
        localStorage.setItem('user', JSON.stringify(this.userData));
        JSON.parse(localStorage.getItem('user')!);
      } 

    });
  }

 // Returns true when user is looged in and email is verified
 get isLoggedIn(): boolean 
 {
    const user = JSON.parse(localStorage.getItem('user')!); // feach data from localstorage
    return user != null ? true : false;
 }

 get isAuthorized(): boolean 
 {
    const role = (localStorage.getItem('role')!); // feach data from localstorage
    
    if(role!=null && role.includes('ResturantAdmin'))
    {
      return true
    }
    return  false;
 }


 get AppOwnerAuthorized(): boolean 
 {
    const role = (localStorage.getItem('role')!); // feach data from localstorage
    
    if(role!=null && role.includes('AppOwner'))
    {
      return true
    }
    return  false;
 }

 // Auth logic to run auth providers
 AuthLogin(provider: any) 
 {
     return this.AFAuth
    .signInWithPopup(provider)
    .then((result) => 
    {
      this.router.navigate(['AllResturantsComponent']);
      this.SetUserData(result.user);
    })
    .catch((error) => {
      window.alert(error);
    });
}

 /* Setting up user data when sign in with username/password, 
  sign up with username/password and sign in with social auth  
  provider in Firestore database using AngularFirestore + AngularFirestoreDocument service */
  SetUserData(user: any) {
    const userRef: AngularFirestoreDocument<any> = this.AFStore.doc(
      `users/${user.email}`
    );
    const userData: Login = {
      email: user.email,
      password : user.password
    };
    return userRef.set(userData, {merge: true,});
  }
}


