import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../model/book';


@Injectable({
  providedIn: 'root',
})
export class APProjectService {
baseUrl: string;
constructor(private http: HttpClient) {
  this.baseUrl = 'http://localhost:5222/api';
}

}


