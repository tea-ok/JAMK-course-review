import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css',
})
export class LoginFormComponent {
  @Output() formSubmit = new EventEmitter<{
    username: string;
    password: string;
  }>();

  username: string = '';
  password: string = '';

  submit() {
    this.formSubmit.emit({ username: this.username, password: this.password });
  }
}
