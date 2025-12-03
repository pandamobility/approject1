import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { Home } from './home/home';
import { BooksPage } from './books-page/books-page';  

export const routes: Routes = [
    {path: '', component: Home},
    {path: 'books-page', component: BooksPage}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }   
