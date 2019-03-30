import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OptionsBarComponent } from './options-bar.component';

describe('OptionsBarComponent', () => {
  let component: OptionsBarComponent;
  let fixture: ComponentFixture<OptionsBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OptionsBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OptionsBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
