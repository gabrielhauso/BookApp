import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { BookList } from './pages/book-list/book-list';
import { BookForm } from './pages/book-form/book-form';
import { Quotes } from './pages/quotes/quotes';
import { authGuard } from './guards/auth.guards';

export const routes: Routes = [
    { path: '', component: BookList, canActivate: [authGuard] },
    { path: 'books/new', component: BookForm, canActivate: [authGuard]},
    { path: 'books/edit/:id', component: BookForm, canActivate: [authGuard]},
    { path: 'quotes', component: Quotes, canActivate: [authGuard]},
    { path: 'login', component: Login},
    { path: 'register', component: Register},
    { path: '**', redirectTo: '' }

];
