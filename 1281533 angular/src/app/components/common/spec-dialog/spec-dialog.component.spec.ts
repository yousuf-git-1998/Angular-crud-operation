import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpecDialogComponent } from './spec-dialog.component';

describe('SpecDialogComponent', () => {
  let component: SpecDialogComponent;
  let fixture: ComponentFixture<SpecDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SpecDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SpecDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
