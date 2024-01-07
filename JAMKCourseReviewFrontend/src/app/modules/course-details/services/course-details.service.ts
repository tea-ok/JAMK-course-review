import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CourseDetailsService {
  private baseUrl = 'http://localhost:5073/api/courses/course';

  constructor(private http: HttpClient) {}

  getCourseDetails(courseCode: string): Observable<any> {
    const url = `${this.baseUrl}?courseCode=${courseCode}`;
    return this.http.get<any>(url);
  }
}
