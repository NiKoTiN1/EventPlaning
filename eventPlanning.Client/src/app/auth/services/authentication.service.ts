import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpRequest,
} from '@angular/common/http';
import { combineLatest, Observable, throwError } from 'rxjs';
import { tap, map, catchError, switchMap } from 'rxjs/operators';
import { AuthService } from 'ngx-auth';
import { TokenStorage } from './token-storage.service';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';
import { AccessData } from 'src/app/shared/models/access-data.model';
import { RegisterModel } from 'src/app/shared/models/register.model';
import { LoginModel } from 'src/app/shared/models/login.model';
import { Role } from 'src/app/shared/models/roles.enum';
import { environment } from 'src/app/environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthenticationService implements AuthService {
  constructor(
    private http: HttpClient,
    private tokenStorage: TokenStorage,
    private router: Router
  ) {}

  public refreshToken(): Observable<AccessData> {
    return combineLatest([
      this.tokenStorage.getAccessToken(),
      this.tokenStorage.getRefreshToken(),
    ]).pipe(
      switchMap(([accessToken, refreshToken]) => {
        return this.http.put<AccessData>(
          `${environment.apiUrl}/token/refresh`,
          {
            accessToken: accessToken,
            refreshToken: refreshToken,
          }
        );
      }),
      tap((accessData: AccessData) => {
        this.tokenStorage.setAccessToken(accessData.accessToken);
        this.tokenStorage.setRefreshToken(accessData.refreshToken);
      }),
      catchError((err) => {
        this.logout();
        this.router.navigateByUrl('/login');
        return this.catchErrorLog(err);
      })
    );
  }

  public refreshShouldHappen(response: HttpErrorResponse): boolean {
    if (response.status !== 401) {
      return false;
    }
    return true;
  }

  public verifyRefreshToken(req: HttpRequest<any>): boolean {
    return req.url.endsWith('refresh');
  }

  public registration(model: RegisterModel): Observable<AccessData> {
    const formData = new FormData();
    formData.append('Email', model.email);
    formData.append('UserName', model.userName);
    formData.append('Password', model.password);
    formData.append('PhoneNumber', model.phoneNumber);
    formData.append('BirthDate', model.birthDate ? model.birthDate.toString() : "");
    formData.append('OrganizationName', model.organizationName);
    formData.append('Website', model?.website);
    formData.append('UserType', model?.userType.toString());

    return this.http
      .post<AccessData>(`${environment.apiUrl}/account/register`, formData)
      .pipe(
        tap((accessData: AccessData) => {
          this.saveAccessData(accessData);
        }),
        catchError((err) => {
          this.logout();
          return this.catchErrorLog(err);
        })
      );
  }

  public logout(): void {
    this.tokenStorage.clear();
  }

  public isAuthorized(): Observable<boolean> {
    return this.tokenStorage.getAccessToken().pipe(map((token) => !!token));
  }

  public getAccessToken(): Observable<string> {
    return this.tokenStorage.getAccessToken();
  }

  public login(model: LoginModel): Observable<AccessData> {
    const formData = new FormData();
    formData.append('Email', model.email);
    formData.append('Password', model.password);

    return this.http
      .post<AccessData>(`${environment.apiUrl}/account/login`, formData)
      .pipe(
        tap((accessData: AccessData) => {
          this.saveAccessData(accessData);
        }),
        catchError((err) => {
          this.logout();
          return this.catchErrorLog(err);
        })
      );
  }

  public getUserId(): Observable<string> {
    return this.getAccessToken().pipe(
      map((token: string) => {
        const decodedToken = jwtDecode<object>(token) as { UserId: string };
        return decodedToken['UserId'];
      })
    );
  }

  public getRole(): Observable<Role> {
    return this.getAccessToken().pipe(
      map((token: string) => {
        const decodedToken = jwtDecode<object>(token) as { Role: string };
        if (decodedToken['Role'] === 'Guest') {
          return Role.Guest;
        }
        return Role.Creator;
      })
    );
  }

  private decodeToken(token: string): object {
    return jwtDecode<object>(token);
  }

  private saveAccessData({ accessToken, refreshToken }: AccessData): void {
    this.tokenStorage.setAccessToken(accessToken);
    this.tokenStorage.setRefreshToken(refreshToken);
  }

  private catchErrorLog(err: Error): Observable<never> {
    return throwError(() => err);
  }
}
