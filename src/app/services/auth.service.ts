import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConfig } from '../config/api.config';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {

  constructor(private http: HttpClient) {}

  login(email: string, senha: string): Observable<any> {
    return this.http.post(ApiConfig.AUTH.LOGIN, { email, senha });
  }

  setUserData(data: any) {
    localStorage.setItem('user', JSON.stringify(data));
    localStorage.setItem('token', data.token);
  }

  getUserData() {
    return JSON.parse(localStorage.getItem('user') || '{}');
  }

  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
  }
} 