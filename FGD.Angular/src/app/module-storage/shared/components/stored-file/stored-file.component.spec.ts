import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StoredFileComponent } from './stored-file.component';

describe('StoredFileComponent', () => {
  let component: StoredFileComponent;
  let fixture: ComponentFixture<StoredFileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StoredFileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StoredFileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
