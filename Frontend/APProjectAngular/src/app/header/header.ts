import { Component } from '@angular/core';
import { BooksPage } from '../books-page/books-page';

@Component({
  selector: 'app-header',
  imports: [BooksPage],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {

}
