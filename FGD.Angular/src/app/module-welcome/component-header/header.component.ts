import { Component, OnInit } from '@angular/core';

import { trigger, state, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  animations: [
    trigger('moveArrow', [
      state('initial', style({
        marginTop: '75px', 
      })),
      state('stage1',style({
        marginTop: '85px', 
      })),
      state('stage2',style({
        marginTop: '95px', 
      })),
      state('final', style({ 
        marginTop: '100px', 
      })),
      transition('final=>initial', animate('0.1s')),
      transition('initial=>stage1', animate('0.05s')),
      transition('stage1=>stage2', animate('0.1s')),
      transition('stage2=>final', animate('0.05s')), 
    ]),
  ]
})
export class HeaderComponent implements OnInit {

  constructor() { }

  innerHeight : number = 0;

  state;

  ngOnInit() {
    this.innerHeight = window.outerHeight; 
    this.state = 'initial';
  }

  onAnimationEnd($event){
    setTimeout(e => {
      
        switch(this.state){
          case "initial" :   this.state = 'stage1' ; break;
          case "stage1" :   this.state = 'stage2' ; break;
          case "stage2" :   this.state = 'final' ; break;
          case "final" :   this.state = 'initial' ; break;
        } 

    },1000);
  }
}
