import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CreateEvent } from 'src/app/shared/models/create-event.model';
import { EventService } from '../services/event.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  createEventForm!: FormGroup;
  date!: Date;

  constructor(
    private _fb: FormBuilder,
    private eventService: EventService
  ) {
  }

  ngOnInit(): void {
    this.createEventForm = this._fb.group({
      eventName: new FormControl('', [Validators.required]),
      eventDescription: new FormControl('', Validators.required),
      startDate: new FormControl('', Validators.required),
      maxCapacity: new FormControl('', Validators.required),
      eventFields: this._fb.array<FormGroup>([])
    });
    this.date = new Date();
  }

  addEventField(): void {
    this.eventFields().push(this.newEventField());
  }

  eventFields(): FormArray<FormGroup> {
    return this.createEventForm.get("eventFields") as FormArray<FormGroup>
  }

  newEventField() {
    return this._fb.group({
      name: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      fieldType: new FormControl('', Validators.required)
    })
  }

  removeEventField(i: number) {
    this.eventFields().removeAt(i);
  }

  create() {
    let model = this.createEventForm.getRawValue() as CreateEvent;
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
