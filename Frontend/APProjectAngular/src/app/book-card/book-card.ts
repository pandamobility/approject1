import { Book } from '../model/book';
import { Component, Input} from '@angular/core';


@Component({
  selector: 'app-book-card',
  imports: [],
  templateUrl: './book-card.html',
  styleUrl: './book-card.css',
})

export class BookCard {
  @Input() book!: Book;
};
