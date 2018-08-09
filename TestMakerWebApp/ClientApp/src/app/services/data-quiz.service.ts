import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';

@Injectable()
export class DataQuizService {

  constructor( private httpClient: HttpClient, private router: Router, @Inject('BASE_URL') private baseUrl: string) {
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

  getQuizzesList(url: string): any {
    return this.httpClient.get<Quiz[]>(url);
  }

  postQuiz(quiz: Quiz) {   
    this.httpClient.post<Quiz>(this.baseUrl, quiz).subscribe(res => {
      let q = res;
      console.log("Quiz " + q.Id + " has been created.");
      this.router.navigate(["home"]);
    }, error => console.error(error));
  }

  putQuiz(quiz: Quiz) {
    this.httpClient.put<Quiz>(this.baseUrl, quiz).subscribe(res => {
      let q = res;
      console.log("Quiz " + q.Id + " has been updated.");
      this.router.navigate(["home"]);
    }, error => console.error(error));
  }

  deleteQuiz(id: number) {
    this.httpClient.delete<Quiz>(this.baseUrl + id).subscribe(res => {
      console.log("Quiz " + id + " has been deleted");
      this.router.navigate(["home"]);
    }, error => console.error(error))
  }
}
