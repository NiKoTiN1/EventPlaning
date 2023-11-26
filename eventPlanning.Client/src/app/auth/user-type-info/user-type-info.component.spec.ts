import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserTypeInfoComponent } from './user-type-info.component';

describe('UserTypeInfoComponent', () => {
  let component: UserTypeInfoComponent;
  let fixture: ComponentFixture<UserTypeInfoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserTypeInfoComponent]
    });
    fixture = TestBed.createComponent(UserTypeInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
