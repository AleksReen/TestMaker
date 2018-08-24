import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { isPlatformBrowser } from '@angular/common';
import 'rxjs/Rx';

@Injectable()
export class AuthService {

  authKey: string = "auth";
  clientId: string = "TestMakerFree";

  constructor(private httpClient: HttpClient, @Inject(PLATFORM_ID) private platformID: any, @Inject('BASE_URL') private baseUrl: string ) {

  }

  refreshToken(): Observable<boolean> {   
    let data = {
      client_id: this.clientId,
      grant_type: "refresh_token",
      refresh_token: this.getAuth()!.refresh_token,
      scope: "offline_access profile email"
    };

    return this.getAuthFromServer(data);
  }

  getAuthFromServer(data: any): Observable<boolean> {
    let url = this.baseUrl + "api/token/auth";

    return this.httpClient.post<TokenResponse>(url, data)
      .map((res) => {
        let token = res && res.token;
        if (token) {
          this.setAuth(res);
          return true;
        }

        return Observable.throw('Unauthorized');
      }).catch(error => {

        return new Observable<any>(error);
      });
  }

  login(username: string, password: string): Observable<boolean> {
   
    let data = {
      username: username,
      password: password,
      client_id: this.clientId,
      grant_type: "password",
      scope: "offline_access profile email"
    };

    return this.getAuthFromServer(data);
  }

  logout(): boolean {
    this.setAuth(null);

    return true;
  }

  setAuth(auth: TokenResponse | null) {
    if (isPlatformBrowser(this.platformID)) {
      if (auth) {
        localStorage.setItem(this.authKey, JSON.stringify(auth));
      }
      else {
        localStorage.removeItem(this.authKey);
      }
    }

    return true;
  }

  getAuth(): TokenResponse | null {
    if (isPlatformBrowser(this.platformID)) {
      var i = localStorage.getItem(this.authKey);
      if (i) {
        return JSON.parse(i);
      }

      return null;
    }
  }

  isLoggedIn(): boolean {
    if (isPlatformBrowser(this.platformID)) {
      return localStorage.getItem(this.authKey) != null;
    }

    return false;
  }
}
