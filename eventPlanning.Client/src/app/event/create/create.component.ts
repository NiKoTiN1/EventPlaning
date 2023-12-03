import { Component, OnInit } from '@angular/core';
import { CreateEvent } from 'src/app/shared/models/create-event.model';
import { EventService } from '../services/event.service';
import { PageMode } from 'src/app/shared/models/page-mode.enum';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  pageModes = PageMode;

  constructor(
    private eventService: EventService
  ) {
  }

  ngOnInit(): void {
  }

  create(model: CreateEvent) {
    this.eventService.createEvent(model).subscribe({
      next: (data: CreateEvent) => {
        console.log("success");
        console.log(data);
        // if (data.accessToken != null) { 
        //   // this.router.navigateByUrl('/');
        // }
      },
      error: (err) => {
        console.log(err);
        // this.errors = this.errors.concat(err.error);
      }
    }
    );
  }
}
