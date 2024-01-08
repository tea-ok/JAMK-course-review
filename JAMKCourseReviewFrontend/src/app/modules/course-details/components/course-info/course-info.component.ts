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

          // Replace newlines with <br> tags
          if (this.course.course.objective) {
            this.course.course.objective = this.course.course.objective.replace(
              /\r?\n/g,
              '<br>'
            );
          }

          if (this.course.avgRatings) {
            this.avgRatingsArray = Object.entries(this.course.avgRatings).map(
              ([key, value]) => ({ key, value: Number(value) })
            );
          } else {
            this.avgRatingsArray = [
              'contentRating',
              'difficultyRating',
              'hoursPerWeek',
              'lectureRating',
              'overallRating',
              'wouldTakeAgainPercentage',
            ].map((key) => ({ key, value: NaN }));
          }
        });
    }
  }
}
