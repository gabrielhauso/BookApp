import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { BookList } from './pages/book-list/book-list';
import { BookForm } from './pages/book-form/book-form';
import { Quotes } from './pages/quotes/quotes';

export const routes: Routes = [
    { path: '', component: BookList},
    { path: 'books/new', component: BookForm},
    { path: 'books/edit/:id', component: BookForm},
    { path: 'quotes', component: Quotes},
    { path: 'login', component: Login},
    { path: 'register', component: Register},
    { path: '**', redirectTo: '' }

];
