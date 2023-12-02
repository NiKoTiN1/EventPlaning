import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventCustomFieldComponent } from './event-custom-field.component';

describe('EventCustomFieldComponent', () => {
  let component: EventCustomFieldComponent;
  let fixture: ComponentFixture<EventCustomFieldComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EventCustomFieldComponent]
    });
    fixture = TestBed.createComponent(EventCustomFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
