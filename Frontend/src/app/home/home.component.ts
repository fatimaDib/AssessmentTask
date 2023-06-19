import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Customer } from '../Models/Customer';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../Services/account.service';
import { UserInfoRequest } from '../Models/UserInfoRequest';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  customers: Customer[] = [];

  customer:Customer=new Customer;

  customerId!:number;
  initialCredits!:number ;

  constructor(public _AccountService :AccountService,private router:Router) { }
  
  ngOnInit(): void {
    this._AccountService.getCustomers().subscribe(
      (resultData)=>{
        this.customers=resultData;
    })
  }
  onIDChange(customerId: any) {
    this.customerId = customerId.target.value;
  }

  search(){
    const userInfoRequest: UserInfoRequest = {
      customerId: this.customerId,
      initialCredits:this.initialCredits,
    };
    this._AccountService.openAccount(userInfoRequest).subscribe(
      (resultData) => {
        this.customer=resultData;
        console.log(this.customer);
        if(this.customer!=null){
          this.router.navigate(['/customerInfos/'+userInfoRequest.customerId]);
        }
        else{
          window.alert("Invalid customerId or initialCredits");
        }
     },
      err => {
        console.log(err);
      }
    );
  }

}
