import { Component } from '@angular/core';
import { BookList } from "../book-list/book-list";
import { BookCard } from "../book-card/book-card";

@Component({
  selector: 'app-books-page',
  imports: [BookList, BookCard],
  templateUrl: './books-page.html',
  styleUrl: './books-page.css',
})
export class BooksPage {

}
