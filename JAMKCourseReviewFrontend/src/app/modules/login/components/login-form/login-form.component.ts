import { Component, Output, EventEmitter, Input } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css',
})
export class LoginFormComponent {
  constructor(private snackBar: MatSnackBar) {}

  @Input() set errorMessage(message: string | null) {
    if (message) {
      // Has to be done, since the error message is in the format "<message> (<timestamp>)"
      // That was done to ensure that the error message is unique, enabling it to be shown multiple times.
      const actualMessage = message.split(' (')[0];
      this.openSnackBar(actualMessage, 'x');
    }
  }

  @Output() formSubmit = new EventEmitter<{
    username: string;
    password: string;
  }>();

  username: string = '';
  password: string = '';

  submit() {
    this.formSubmit.emit({ username: this.username, password: this.password });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 5000,
    });
  }
}
