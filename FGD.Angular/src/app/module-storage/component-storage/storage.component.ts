import { Component, OnInit, OnDestroy } from '@angular/core';
import { SignalRService } from 'src/app/shared/api/signalR.service';

@Component({
  selector: 'app-storage',
  templateUrl: './storage.component.html',
  styleUrls: ['./storage.component.css']
})
export class StorageComponent implements OnInit,OnDestroy {

  constructor(private signalRService:SignalRService) { }

  ngOnInit(): void {
    this.signalRService.Start();
  }

  ngOnDestroy(): void {
    this.signalRService.Stop();
  }
 
}
