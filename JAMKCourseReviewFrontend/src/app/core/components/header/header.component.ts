import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  constructor(
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  logout() {
    this.authService.logout().subscribe(() => {
      this.router.navigate(['']);
      this.snackBar.open('Logged out successfully', 'x', {
        duration: 5000,
      });
    });
  }
}
