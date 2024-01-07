import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CourseDetailsRoutingModule } from './course-details-routing.module';
import { CourseDetailsComponent } from './pages/course-details/course-details.component';
import { CourseInfoComponent } from './components/course-info/course-info.component';

import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { RatingBoxComponent } from './components/rating-box/rating-box.component';

@NgModule({
  declarations: [CourseDetailsComponent, CourseInfoComponent, RatingBoxComponent],
  imports: [
    CommonModule,
    CourseDetailsRoutingModule,
    MatCardModule,
    MatGridListModule,
  ],
})
export class CourseDetailsModule {}
