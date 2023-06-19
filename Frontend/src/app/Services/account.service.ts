import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { UserInfoRequest } from '../Models/UserInfoRequest';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http :HttpClient) { }

  getCustomers(): Observable<any> {
    return this.http.get("https://localhost:7123/api/Customer").pipe(
      catchError(this.handleError)
    );
  }
  getuserInfos(customerId:any): Observable<any> {
    const url = `https://localhost:7123/api/Customer/user-information?customerId=${customerId}`;
    return this.http.get(url).pipe(
      catchError(this.handleError)
    );
  }

  openAccount(userInfoRequest:UserInfoRequest): Observable<any> {
    return this.http.post("https://localhost:7123/api/Customer",userInfoRequest).pipe(
      catchError(this.handleError)
    );
  }

  handleError(error: HttpErrorResponse){
    if(error.status===500){
      console.log("Error Occured in httpRequest");
    }

    if(error.status===400){
      console.log("Bad Request");
    }

    if(error.status===200){
      console.log("OK");
    }

    if(error.status===404){
      console.log("Not found");
    }
    return throwError(error.status);
  }
}
