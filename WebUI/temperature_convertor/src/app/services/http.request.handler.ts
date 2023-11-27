import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class HttpRequestHandler {
    constructor(private http: HttpClient) {
    }

    post<T>(url: string, data: any):  Observable<HttpResponse<T>> {
        let body = JSON.stringify(data);
        return this.http.post<T>(url, body, { headers: this.createHeaders(), observe: 'response' });
    }

    put<T>(url: string, data: any): Observable<T> {
        let body = JSON.stringify(data);
        return this.http.put<T>(url, body, { headers: this.createHeaders() });
    }
    get<T>(url: string): Observable<T> {
        return this.http.get<T>(url);
    }

    private createHeaders(): any {
        let defaultHeaders: any = {
            'content-type': 'application/json'
        };

        return new HttpHeaders(defaultHeaders)

    }
}