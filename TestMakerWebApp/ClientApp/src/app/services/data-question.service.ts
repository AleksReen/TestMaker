import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class DataQuestionService {

  constructor(private httpClient: HttpClient, private router: Router, @Inject('BASE_URL') private baseUrl: string) {
    this.baseUrl = baseUrl + "api/question/";
  }

  loadData(quizId: number): Observable<Question []> {
    var url = this.baseUrl + "All/" + quizId;
    return this.httpClient.get<Question[]>(url);
  }

  deleteQuestion(questionId: number): any {
    var url = this.baseUrl + questionId;
    return this.httpClient.delete(url);
  }

  getQuestion(id: number): Observable<Question> {
    var url = this.baseUrl + id;
    return this.httpClient.get<Question>(url);
  }

  postQuestion(question: Question): void {
    this.httpClient.post<Question>(this.baseUrl, question).subscribe(res => {
      let v = res;
      console.log("Question " + v.Id + " has been create.");
      this.router.navigate(["quiz/edit", v.QuizId]);
    }, er => console.error(er));
  }

  putQuestion(question: Question): void {
    this.httpClient.put<Question>(this.baseUrl, question).subscribe(res => {
      let v = res;
      console.log("Question " + v.Id + " has been updated.");
      this.router.navigate(["quiz/edit", v.QuizId]);
    }, er => console.error(er));
  }

}
