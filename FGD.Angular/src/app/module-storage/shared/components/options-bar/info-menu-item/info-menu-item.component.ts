import { Component, OnInit, OnDestroy  } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet'; 
import { InfoBottomSheet } from './info-bottom-sheet/info-bottom-sheet.component';

@Component({
    selector: 'info-menu-item',
    templateUrl: 'info-menu-item.component.html',
  })
  export class InfoMenuItem implements OnInit, OnDestroy{

  ngOnInit(): void {
  }

    constructor(private bottomSheet: MatBottomSheet) {}
  
    public onInfoClicked(){ 
 
      this.bottomSheet.open(InfoBottomSheet)
      
    }

    ngOnDestroy(): void {
    }
  }

   