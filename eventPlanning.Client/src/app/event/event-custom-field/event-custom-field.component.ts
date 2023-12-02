import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FieldType } from 'src/app/shared/models/field-type.enum';

@Component({
  selector: 'app-event-custom-field',
  templateUrl: './event-custom-field.component.html',
  styleUrls: ['./event-custom-field.component.css']
})
export class EventCustomFieldComponent {
  @Input() form!: FormGroup;
  @Input() id!: number;
  @Output() delete: EventEmitter<number> = new EventEmitter<number>();

  currentfieldType!: FieldType;
  fieldTypes = FieldType;
  enumKeys!: number[];
  inputType: string = 'text';

  constructor() {
    this.enumKeys = Object.keys(this.fieldTypes).filter((f) => !isNaN(Number(f))).map(value => Number(value));
    this.currentfieldType = FieldType.String
  }

  changeInputType(value: FieldType) {
    switch (value) {
      case FieldType.Date:
        this.inputType = 'date';
        break;
      case FieldType.Int:
        this.inputType = 'number';
        break;
      case FieldType.String:
        this.inputType = 'text';
        break;
    }
  }

  deleteField() {
    this.delete.emit(this.id);
  }
}
