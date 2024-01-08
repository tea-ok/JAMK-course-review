import { Component, OnInit } from '@angular/core';
import { map, startWith } from 'rxjs/operators';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { CourseService } from '../../services/course.service';
import { Course } from '../../../../shared/models/course.model';
import { Router } from '@angular/router';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';

@Component({
  selector: 'app-searchbar',
  templateUrl: './searchbar.component.html',
  styleUrls: ['./searchbar.component.css'],
})
export class SearchbarComponent implements OnInit {
  myControl = new FormControl();
  options: Course[] = [];
  filteredOptions!: Observable<Course[]>;

  constructor(private courseService: CourseService, private router: Router) {}

  ngOnInit() {
    this.courseService.getCourses().subscribe((data) => {
      this.options = data;
      this.filteredOptions = this.myControl.valueChanges.pipe(
        startWith(''),
        map((value) => this._filter(value))
      );
    });
  }

  private _filter(value: string): Course[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(
      (option) =>
        option.course.courseTitle.toLowerCase().includes(filterValue) ||
        option.course.courseCode.toLowerCase().includes(filterValue)
    );
  }

  onOptionSelected(event: MatAutocompleteSelectedEvent) {
    const courseCode = event.option.value;
    this.router.navigate(['/course-details', courseCode]);
  }
}
