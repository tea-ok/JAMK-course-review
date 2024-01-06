import { Component } from '@angular/core';
import { AuthService } from '../../../../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private router: Router) {}

  onFormSubmit({ username, password }: { username: string; password: string }) {
    this.authService.login(username, password).subscribe({
      next: (response) => {
        console.log('Login successful', response);
        this.errorMessage = null;

        // Redirect back to search page
        this.router.navigate(['']);
      },
      error: (error) => {
        console.log('Login failed', error);
        this.errorMessage = `${error.error.message} (${new Date().getTime()})`;
      },
    });
  }
}
