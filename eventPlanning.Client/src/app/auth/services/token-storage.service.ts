import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class TokenStorage {
  private readonly accessToken = new BehaviorSubject<string>(
    <string>localStorage.getItem('accessToken')
  );

  public getAccessToken(): Observable<string> {
    return this.accessToken;
  }

  public getRefreshToken(): Observable<string> {
    const token: string = <string>localStorage.getItem('refreshToken');
    return of(token);
  }

  public setAccessToken(token: string): void {
    localStorage.setItem('accessToken', token);
    this.accessToken.next(token);
  }

  public setRefreshToken(token: string): void {
    localStorage.setItem('refreshToken', token);
  }

  public clear(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    this.setToken(null!);
  }

  private setToken(token: string): void {
    this.accessToken.next(token);
  }
}
