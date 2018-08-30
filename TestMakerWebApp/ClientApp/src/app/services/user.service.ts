import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class UserService {

  url: string

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.url = this.baseUrl + "api/user";
  }

  saveUser(user: User): Observable<User> {
    return this.http.post<User>(this.url, user);    
  }

}
