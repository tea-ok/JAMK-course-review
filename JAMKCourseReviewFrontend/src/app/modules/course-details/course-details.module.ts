import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CourseDetailsRoutingModule } from './course-details-routing.module';
import { CourseDetailsComponent } from './pages/course-details/course-details.component';
import { CourseInfoComponent } from './components/course-info/course-info.component';


@NgModule({
  declarations: [
    CourseDetailsComponent,
    CourseInfoComponent
  ],
  imports: [
    CommonModule,
    CourseDetailsRoutingModule
  ]
})
export class CourseDetailsModule { }
