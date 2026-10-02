import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Book, BookRequest } from '../models/book';

@Injectable({ providedIn: 'root' })
export class BookService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Book`;

  getAll(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl);
  }

  getById(id: number): Observable<Book> {
    return this.http.get<Book>(`${this.apiUrl}/${id}`);
  }

  create(book: BookRequest): Observable<Book> {
    return this.http.post<Book>(this.apiUrl, book);
  }

  update(id: number, book: BookRequest): Observable<Book> {
    return this.http.put<Book>(`${this.apiUrl}/${id}`, book);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}