import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  private authService = inject(AuthService);
  private router = inject(Router);

  username = '';
  password = '';
  errorMessage = '';

  onSubmit(): void {
    this.errorMessage = '';

    this.authService.register(this.username, this.password).subscribe({
      next: () => this.router.navigate(['/login']),
      error: (err) => {
        if (err.status === 409) {
          this.errorMessage = 'Användrnamnet är upptagen';
        } else if (err.status === 400) {
          this.errorMessage = 'Fyll i användarnamn och lösenord';
        } else {
          this.errorMessage = 'kunde inte nå servern'
        }
      }
    });
  }

}
