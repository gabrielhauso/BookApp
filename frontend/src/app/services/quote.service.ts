import { Injectable, inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Quote, QuoteRequest } from "../models/quote";

@Injectable({ providedIn: 'root' })
export class QuoteService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Quote`;

  getAll(): Observable<Quote[]> {
    return this.http.get<Quote[]>(this.apiUrl);
  }

  create(quote: QuoteRequest): Observable<Quote> {
    return this.http.post<Quote>(this.apiUrl, quote);
  }

  update(id: number, quote: QuoteRequest): Observable<Quote> {
    return this.http.put<Quote>(`${this.apiUrl}/${id}`, quote);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}