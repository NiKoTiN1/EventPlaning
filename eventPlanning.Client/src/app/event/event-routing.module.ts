import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { AllComponent } from './all/all.component';

const routes: Routes = [
  { path: 'event/create', component: CreateComponent },
  { path: 'event/all', component: AllComponent },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule],
})
export class EventRoutingModule { }
