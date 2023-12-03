import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { EventModel } from 'src/app/shared/models/event.model';
import { PageMode } from 'src/app/shared/models/page-mode.enum';
import { EventField } from 'src/app/shared/models/event-field.model';
import { FieldType } from 'src/app/shared/models/field-type.enum';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css']
})
export class EventComponent implements OnInit {
  @Input() event!: EventModel;
  @Input() pageMode!: PageMode;
  @Output() submitEvent: EventEmitter<EventModel> = new EventEmitter<EventModel>();
  @Output() updateEvent: EventEmitter<EventModel> = new EventEmitter<EventModel>();

  eventForm!: FormGroup;
  pageModes = PageMode;
  constructor(
    private _fb: FormBuilder,
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  addEventField(): void {
    this.eventFields().push(this.newEventField());
  }

  eventFields(): FormArray<FormGroup> {
    return this.eventForm.get("eventFields") as FormArray<FormGroup>
  }

  newEventField(eventField: EventField | null = null) {
    if (eventField) {
      const isDisabled = this.pageMode === PageMode.View;

      return this._fb.group({
        name: [{value: eventField.name, disabled: isDisabled}, Validators.required],
        description: [{value: eventField.description, disabled: isDisabled}, Validators.required],
        fieldType: [{value: eventField.fieldType, disabled: isDisabled}, Validators.required]
      })
    }

    return this._fb.group({
      name: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      fieldType: new FormControl('', Validators.required)
    })
  }

  removeEventField(i: number) {
    this.eventFields().removeAt(i);
  }

  submit() {
    this.event = this.eventForm.getRawValue() as EventModel;
    this.submitEvent.emit(this.event);
  }

  private initializeEventFields(eventFields: EventField[]): FormArray<FormGroup<any>> {
    let fileds = this._fb.array<FormGroup>([]);
    for (let filed of eventFields) {
      fileds.push(this.newEventField(filed))
    }

    return fileds;
  }

  private initializeForm() {
    switch (this.pageMode) {
      case PageMode.Create:
        this.eventForm = this._fb.group({
          eventName: new FormControl('', [Validators.required]),
          eventDescription: new FormControl('', Validators.required),
          startDate: new FormControl('', Validators.required),
          maxCapacity: new FormControl('', Validators.required),
          eventFields: this._fb.array<FormGroup>([])
        });
        break;
      case PageMode.Update:
      case PageMode.View:
        const isDisabled = this.pageMode === PageMode.View;
        this.eventForm = this._fb.group({
          eventName: [{value: this.event.eventName, disabled: isDisabled}, [Validators.required]],
          eventDescription: [{value: this.event.eventDescription, disabled: isDisabled}, Validators.required],
          startDate: [{value: this.event.startDate, disabled: isDisabled}, Validators.required],
          maxCapacity: [{value: this.event.maxCapacity, disabled: isDisabled}, Validators.required],
          eventFields: this.initializeEventFields(this.event.eventFields)
        });
        break;
    }
  }
}
