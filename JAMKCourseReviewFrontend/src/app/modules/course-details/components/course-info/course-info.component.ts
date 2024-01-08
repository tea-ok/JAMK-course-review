import { Component, Input } from '@angular/core';
import { CourseDetailsService } from '../../services/course-details.service';
import { KeyValue } from '@angular/common';

@Component({
  selector: 'app-course-info',
  templateUrl: './course-info.component.html',
  styleUrl: './course-info.component.css',
})
export class CourseInfoComponent {
  @Input() courseCode: string | null = null;
  avgRatingsArray: KeyValue<string, number>[] = [];
  course: any = null;

  constructor(private courseDetailsService: CourseDetailsService) {}

  ngOnInit() {
    if (this.courseCode) {
      this.courseDetailsService
        .getCourseDetails(this.courseCode)
        .subscribe((course) => {
          this.course = course;
          console.log('Course details: ', this.course);
          this.avgRatingsArray = Object.entries(this.course.avgRatings).map(
            ([key, value]) => ({ key, value: Number(value) })
          );
        });
    }
  }
}
