import { Component, OnInit } from '@angular/core';
import { EventService } from '../services/event.service';
import { EventModel } from 'src/app/shared/models/event.model';
import { PageMode } from 'src/app/shared/models/page-mode.enum';

@Component({
  selector: 'app-all',
  templateUrl: './all.component.html',
  styleUrls: ['./all.component.css']
})
export class AllComponent implements OnInit {
  events!: EventModel[];
  pageModes = PageMode;
  constructor(private eventService: EventService) { }
  
  ngOnInit(): void {
    this.eventService.getAllEvents()
      .subscribe(x => { 
          this.events = x 
        });
  }
}
