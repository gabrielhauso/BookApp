import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { BookService } from '../../services/book.service';
import { BookRequest } from '../../models/book';

@Component({
  selector: 'app-book-form',
  imports: [FormsModule, RouterLink],
  templateUrl: './book-form.html',
  styleUrl: './book-form.css',
})
export class BookForm implements OnInit {
  private bookService = inject(BookService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  bookId: number | null = null;
  book: BookRequest = { title: '', author: '', publishedDate: ''};
  errorMessage = '';

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id){
      this.bookId = Number(id);

      this.bookService.getById(this.bookId).subscribe({
        next: (book) => {
          this.book = {
            title: book.title,
            author: book.author,
            publishedDate: book.publishedDate.substring(0, 10)
          };
        },
        error: () => this.errorMessage = 'Kunde inte hämta boken'
      });
    }
  }

   onSubmit(): void {
    this.errorMessage = '';

    const request = this.bookId
      ? this.bookService.update(this.bookId, this.book)
      : this.bookService.create(this.book);

    request.subscribe({
      next: () => this.router.navigate(['/']),
      error: () => this.errorMessage = 'Kunde inte spara boken'
    });
  }

}


