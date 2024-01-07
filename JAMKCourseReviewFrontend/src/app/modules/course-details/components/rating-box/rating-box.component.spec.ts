import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RatingBoxComponent } from './rating-box.component';

describe('RatingBoxComponent', () => {
  let component: RatingBoxComponent;
  let fixture: ComponentFixture<RatingBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RatingBoxComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RatingBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
