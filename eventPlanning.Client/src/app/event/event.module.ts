import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { EventRoutingModule } from './event-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { EventCustomFieldComponent } from './event-custom-field/event-custom-field.component';
import { MatIconModule } from '@angular/material/icon';
import { AllComponent } from './all/all.component';
import { EventComponent } from './event/event.component';



@NgModule({
  declarations: [
    CreateComponent,
    EventCustomFieldComponent,
    AllComponent,
    EventComponent
  ],
  imports: [
    EventRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
    MatIconModule,
    CommonModule
  ]
})
export class EventModule { }
