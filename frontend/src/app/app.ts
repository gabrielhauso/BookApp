import { Component, inject, OnInit, signal } from '@angular/core';
import { Router, RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('frontend');
  protected authService = inject(AuthService);
  private router = inject(Router);

  isDark = false;

  ngOnInit(): void {
    this.isDark = localStorage.getItem('theme') === 'dark';
    this.applyTheme();
  }

  toggleTheme(): void {
    this.isDark = !this.isDark;
    localStorage.setItem('theme', this.isDark ? 'dark' : 'light');
    this.applyTheme();
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  private applyTheme(): void {
    document.documentElement.setAttribute('data-bs-theme', this.isDark ? 'dark' : 'light');
  }
}