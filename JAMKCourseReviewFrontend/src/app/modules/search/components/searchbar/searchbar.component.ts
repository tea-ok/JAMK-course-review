import { Component, OnInit } from '@angular/core';
import { map, startWith } from 'rxjs/operators';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'app-searchbar',
  templateUrl: './searchbar.component.html',
  styleUrls: ['./searchbar.component.css'],
})
export class SearchbarComponent implements OnInit {
  myControl = new FormControl();
  options: { name: string; code: string }[] = [];
  filteredOptions!: Observable<{ name: string; code: string }[]>;

  constructor(private courseService: CourseService) {}

  ngOnInit() {
    this.courseService.getCourses().subscribe((courses) => {
      this.options = (courses as any[]).map((course) => ({
        name: `${course.course.courseTitle} - ${course.course.courseCode}`,
        code: course.course.courseCode,
      }));
      this.filteredOptions = this.myControl.valueChanges.pipe(
        startWith(''),
        map((value) => this._filter(value))
      );
    });
  }

  private _filter(value: string): { name: string; code: string }[] {
    const filterValue = value.toLowerCase();

    return this.options.filter((option) =>
      option.name.toLowerCase().includes(filterValue)
    );
  }
}
