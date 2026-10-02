import { Component, inject, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { BookService } from '../../services/book.service';
import { Book } from '../../models/book';

@Component({
  selector: 'app-book-list',
  imports: [RouterLink, DatePipe],
  templateUrl: './book-list.html',
  styleUrl: './book-list.css',
})
export class BookList implements OnInit {
  private bookService = inject(BookService);

  books: Book[] = [];
  errorMessage = '';

  ngOnInit(): void {
    this.bookService.getAll().subscribe({
      next: (books) => this.books = books,
      error: () => this.errorMessage = 'Kunde inte hämta böckerna'
    });
  }

   deleteBook(book: Book): void {
    if (!confirm(`Vill du ta bort "${book.title}"?`)) {
      return;
    }

    this.bookService.delete(book.id).subscribe({
      next: () => this.books = this.books.filter(b => b.id !== book.id),
      error: () => this.errorMessage = 'Kunde inte ta bort boken'
    });
  }
}
