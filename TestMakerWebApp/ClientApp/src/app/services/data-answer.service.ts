import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class DataAnswerService {

  constructor(private httpClient: HttpClient, private router: Router, @Inject('BASE_URL') private baseUrl: string) {
    this.baseUrl = baseUrl + "api/answer/";
  }

  loadData(questionId: number): Observable<Answer[]> {
    var url = this.baseUrl + "All/" + questionId;
    return this.httpClient.get<Answer[]>(url);
  }

  deleteAnswer(answerId: number): any {
    var url = this.baseUrl + answerId;
    return this.httpClient.delete(url);
  }

  getAnswer(id: number): Observable<Answer> {
    var url = this.baseUrl + id;
    return this.httpClient.get<Answer>(url);
  }

  postAnswer(answer: Answer): void {
    this.httpClient.post<Answer>(this.baseUrl, answer).subscribe(res => {
      let v = res;
      console.log("Answer " + v.Id + " has been create.");
      this.router.navigate(["question/edit", v.QuestionId]);
    }, er => console.error(er));
  }

  putAnswer(answer: Answer): void {
    this.httpClient.put<Answer>(this.baseUrl, answer).subscribe(res => {
      let v = res;
      console.log("Answer " + v.Id + " has been updated.");
      this.router.navigate(["question/edit", v.QuestionId]);
    }, er => console.error(er));
  }
}
