import { Component, Input } from '@angular/core';
import { CourseDetailsService } from '../../services/course-details.service';

@Component({
  selector: 'app-course-info',
  templateUrl: './course-info.component.html',
  styleUrl: './course-info.component.css',
})
export class CourseInfoComponent {
  @Input() courseCode: string | null = null;
  course: any = null;

  constructor(private courseDetailsService: CourseDetailsService) {}

  ngOnInit() {
    if (this.courseCode) {
      this.courseDetailsService
        .getCourseDetails(this.courseCode)
        .subscribe((course) => {
          this.course = course;
        });
    }
    console.log('Course details: ', this.course);
  }
}
