import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  @Input() title:string="";
  @Input() aligment:string="center";//ToDo: create enum or remove it

  getStyle(){

    switch(this.aligment){
      case "center":
        break;
      case "left":
        return 
          `
          justify-content: start!important;
          `;
      default: return;
    }

  }

}
