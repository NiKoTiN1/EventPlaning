import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { AllComponent } from './all/all.component';
import { protectedGuard, publicGuard } from 'ngx-auth';

const routes: Routes = [
  { path: 'event/create', component: CreateComponent, canActivate: [protectedGuard], },
  { path: 'event/all', component: AllComponent },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule],
})
export class EventRoutingModule { }
