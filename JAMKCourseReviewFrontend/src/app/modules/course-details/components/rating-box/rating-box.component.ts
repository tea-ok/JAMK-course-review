import { KeyValue } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-rating-box',
  templateUrl: './rating-box.component.html',
  styleUrl: './rating-box.component.css',
})
export class RatingBoxComponent {
  @Input() rating: KeyValue<string, number> = { key: '', value: 0 };
}
