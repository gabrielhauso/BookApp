import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { QuoteService } from '../../services/quote.service';
import { Quote, QuoteRequest } from '../../models/quote';

@Component({
  selector: 'app-quotes',
  imports: [FormsModule],
  templateUrl: './quotes.html',
  styleUrl: './quotes.css'
})
export class Quotes implements OnInit {
  private quoteService = inject(QuoteService);

  quotes: Quote[] = [];
  form: QuoteRequest = { text: '', author: '' };
  editingId: number | null = null;
  showForm = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadQuotes();
  }

  loadQuotes(): void {
    this.quoteService.getAll().subscribe({
      next: (quotes) => this.quotes = quotes,
      error: () => this.errorMessage = 'Kunde inte hämta citaten'
    });
  }

  openNew(): void {
    this.editingId = null;
    this.form = { text: '', author: '' };
    this.showForm = true;
  }

  startEdit(quote: Quote): void {
    this.editingId = quote.id;
    this.form = { text: quote.text, author: quote.author };
    this.showForm = true;
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  cancelEdit(): void {
    this.editingId = null;
    this.form = { text: '', author: '' };
    this.showForm = false;
  }

  onSubmit(): void {
    this.errorMessage = '';

    const request = this.editingId
      ? this.quoteService.update(this.editingId, this.form)
      : this.quoteService.create(this.form);

    request.subscribe({
      next: () => {
        this.cancelEdit();
        this.loadQuotes();
      },
      error: () => this.errorMessage = 'Kunde inte spara citatet'
    });
  }

  deleteQuote(quote: Quote): void {
    if (!confirm('Vill du ta bort citatet?')) {
      return;
    }

    this.quoteService.delete(quote.id).subscribe({
      next: () => this.quotes = this.quotes.filter(q => q.id !== quote.id),
      error: () => this.errorMessage = 'Kunde inte ta bort citatet'
    });
  }
}