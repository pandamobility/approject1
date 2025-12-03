import { Component } from '@angular/core';
import { Book } from '../model/book';
import { BookCard } from '../book-card/book-card';

@Component({
  selector: 'app-book-list',
  imports: [BookCard],
  templateUrl: './book-list.html',
  styleUrl: './book-list.css',
})
export class BookList {

  books: Book[]=[
  {
  book_id: 1,
  book_title: 'The Hobbit',
  author_id: 5,
  genre_id: 1,
  publisher_id: 1,
  review: 'boring',
  rating: 2,
  page_count: 310,
  year_published: 1937
},
{
  book_id: 2,
  book_title: 'Lord of the Rings',
  author_id: 5,
  genre_id: 1,
  publisher_id: 1,
  review: 'exciting',
  rating: 5,
  page_count: 1077,
  year_published: 1954
},
  {
  book_id: 3,
  book_title: 'Harry Potter and the Philosopher\'s Stone',
  author_id: 1,
  genre_id: 1,
  publisher_id: 1,
  review: 'yoo',
  rating: 10,
  page_count: 223,
  year_published: 1997
}
  ];
}
