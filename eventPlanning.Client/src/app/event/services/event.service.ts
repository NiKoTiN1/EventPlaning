import { Injectable } from "@angular/core";
import { Observable, catchError, tap, throwError } from "rxjs";
import { CreateEvent } from "src/app/shared/models/create-event.model";
import {
    HttpClient,
    HttpErrorResponse,
    HttpRequest,
} from '@angular/common/http';
import { Router } from "@angular/router";
import { environment } from "src/app/environments/environment";

@Injectable({
    providedIn: 'any',
})
export class EventService {
    constructor(
        private http: HttpClient,
        private router: Router
    ) { }
    public createEvent(model: CreateEvent): Observable<CreateEvent> {
        return this.http
            .post<CreateEvent>(`${environment.apiUrl}/event/create`, JSON.stringify(model), {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .pipe(
                tap((accessData: CreateEvent) => {
                    return accessData
                }),
                catchError((err) => {
                    console.log(err);
                    return this.catchErrorLog(err);
                    // this.logout();
                })
            );
    }

    private catchErrorLog(err: Error): Observable<never> {
        return throwError(() => err);
    }
}
