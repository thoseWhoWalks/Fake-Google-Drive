import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StoredFolderComponent } from './stored-folder.component';

describe('StoredFolderComponent', () => {
  let component: StoredFolderComponent;
  let fixture: ComponentFixture<StoredFolderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StoredFolderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StoredFolderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
