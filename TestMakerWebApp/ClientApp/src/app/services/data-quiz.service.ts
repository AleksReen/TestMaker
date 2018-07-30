import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class DataQuizService {

  title: string;
  quizzes: Quiz[] = [];

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.baseUrl = baseUrl + "api/quiz/";
  }

  getRandom(): Observable<Quiz[]> {
    return this.getQuizzesList(this.baseUrl + "Random/");   
  }

  getByTitle(): Observable<Quiz[]> {
    return this.getQuizzesList(this.baseUrl + "ByTitle/");
  }

  getLatest(): Observable<Quiz []> {
    return this.getQuizzesList(this.baseUrl + "Latest/");
  }

  getQuizById(id:number): Observable<Quiz> {
    return this.httpClient.get<Quiz>(this.baseUrl + id);
  }

  private getQuizzesList(url: string): any {
    return this.httpClient.get<Quiz[]>(url);
  }
}
