import { Injectable } from "@angular/core";
import { Observable, catchError, map, tap, throwError } from "rxjs";
import { CreateEvent } from "src/app/shared/models/create-event.model";
import {
    HttpClient,
    HttpErrorResponse,
    HttpRequest,
} from '@angular/common/http';
import { Router } from "@angular/router";
import { environment } from "src/app/environments/environment";
import { EventModel } from "src/app/shared/models/event.model";

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

    public getAllEvents(): Observable<EventModel[]> {
        return this.http
            .get<EventModel[]>(`${environment.apiUrl}/event/all`)
            .pipe(
                map((event: EventModel[]) => event as EventModel[]),
                catchError((err) => this.catchErrorLog(err))
            );;
    }

    private catchErrorLog(err: Error): Observable<never> {
        return throwError(() => err);
    }
}
