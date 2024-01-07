import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-course-details',
  templateUrl: './course-details.component.html',
  styleUrl: './course-details.component.css',
})
export class CourseDetailsComponent {
  courseCode: string | null = null;

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.courseCode = this.route.snapshot.paramMap.get('courseCode');
  }
}
