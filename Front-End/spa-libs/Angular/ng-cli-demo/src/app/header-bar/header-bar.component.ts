import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account/service/account.service';

@Component({
  selector: 'grn-header-bar',
  templateUrl: './header-bar.component.html',
  styleUrls: ['./header-bar.component.scss']
})
export class HeaderBarComponent implements OnInit {

  constructor(
	  private accountService: AccountService
  ) { }

  ngOnInit() {
  }

}
