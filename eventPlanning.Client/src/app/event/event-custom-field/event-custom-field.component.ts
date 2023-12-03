import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FieldType } from 'src/app/shared/models/field-type.enum';
import { PageMode } from 'src/app/shared/models/page-mode.enum';

@Component({
  selector: 'app-event-custom-field',
  templateUrl: './event-custom-field.component.html',
  styleUrls: ['./event-custom-field.component.css']
})
export class EventCustomFieldComponent implements OnInit {
  @Input() form!: FormGroup;
  @Input() id!: number;
  @Input() pageMode!: PageMode;
  @Output() delete: EventEmitter<number> = new EventEmitter<number>();

  currentfieldType!: FieldType;
  fieldTypes = FieldType;
  pageModes = PageMode;
  enumKeys!: number[];
  inputType!: string;

  constructor() {}

  ngOnInit(): void {
    this.enumKeys = Object.keys(this.fieldTypes).filter((f) => !isNaN(Number(f))).map(value => Number(value));
    this.currentfieldType = this.form.get("fieldType")?.value as FieldType;
    this.changeInputType(this.currentfieldType);
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
