import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class DataResultService {

  constructor(private httpClient: HttpClient, private router: Router, @Inject('BASE_URL') private baseUrl: string) {
    this.baseUrl = baseUrl + "api/result/";
  }

  loadData(quizId: number): Observable<Result[]> {
    var url = this.baseUrl + "All/" + quizId;
    return this.httpClient.get<Result[]>(url);
  }

  deleteResult(resultId: number): any {
    var url = this.baseUrl + resultId;
    return this.httpClient.delete(url);
  }

  getResult(id: number): Observable<Result> {
    var url = this.baseUrl + id;
    return this.httpClient.get<Result>(url);
  }

  postResult(result: Result): void {
    this.httpClient.post<Result>(this.baseUrl, result).subscribe(res => {
      let v = res;
      console.log("Result " + v.Id + " has been create.");
      this.router.navigate(["quiz/edit", v.QuizId]);
    }, er => console.error(er));
  }

  putResult(result: Result): void {
    this.httpClient.put<Result>(this.baseUrl, result).subscribe(res => {
      let v = res;
      console.log("Result " + v.Id + " has been updated.");
      this.router.navigate(["quiz/edit", v.QuizId]);
    }, er => console.error(er));
  }

}
