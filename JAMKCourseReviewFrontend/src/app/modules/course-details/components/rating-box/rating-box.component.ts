import { KeyValue } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-rating-box',
  templateUrl: './rating-box.component.html',
  styleUrl: './rating-box.component.css',
})
export class RatingBoxComponent {
  @Input() rating: KeyValue<string, number> = { key: '', value: 0 };

  keyMapping: { [key: string]: string } = {
    contentRating: 'Content Rating',
    difficultyRating: 'Difficulty Rating',
    hoursPerWeek: 'Hours Per Week',
    lectureRating: 'Lecture Rating',
    overallRating: 'Overall Rating',
    wouldTakeAgainPercentage: 'Would Take Again',
  };

  // Custom subtitles for rating boxes
  get subtitle() {
    return this.keyMapping[this.rating.key] || this.rating.key;
  }
}
