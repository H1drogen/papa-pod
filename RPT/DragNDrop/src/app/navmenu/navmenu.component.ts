import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navmenu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.css']
})
export class NavmenuComponent implements OnInit {
  master = 'Master';
  allowNewServer = false;
  // dateIs = Date();


  static userName: string | null;
  constructor(private route: Router) {
    setTimeout(() => { this.allowNewServer = true }, 2000);
  }

  ngOnInit(): void {
    this.route.events.subscribe((val: any) => {
      if (val.url) {
        console.log(val.url)
      }

    })
    // setInterval(()=>{this.dateIs = Date();},1000);
  }

}
