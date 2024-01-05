import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CourseService {
  constructor(private http: HttpClient) {}

  getCourses(): Observable<string[]> {
    return this.http.get<string[]>('http://localhost:5073/api/courses'); // GET all courses
  }
}
