import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../Services/account.service';

@Component({
  selector: 'app-customer-info',
  templateUrl: './customer-info.component.html',
  styleUrls: ['./customer-info.component.css']
})
export class CustomerInfoComponent implements OnInit {
  userInformation: any;
  constructor(public _AccountService :AccountService,private route: ActivatedRoute) { }

  ngOnInit(): void {
    if(this.route.snapshot.params['id'] !==undefined){
      this._AccountService.getuserInfos(this.route.snapshot.params['id']).subscribe(
        (response)=>{
          this.userInformation= response;
        },
        (error)=>{
          console.log(error);
        }
      );
  }
   
  }

}
