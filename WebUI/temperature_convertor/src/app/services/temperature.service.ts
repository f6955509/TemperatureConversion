import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpXsrfTokenExtractor, HttpResponse } from '@angular/common/http';
import { HttpRequestHandler } from "./http.request.handler";
import { TemperatureType } from "../models/temperature.type";

@Injectable()
export class TemperatureService {
    constructor(private httpRequestHandler: HttpRequestHandler, private http: HttpClient){}

    public convertTemperature(InputType: TemperatureType, degree: number): Observable<any>{
        const body = { "InputType": InputType, "InputDegree": degree };
        const url = 'https://localhost:7272/api/Temperature/convert';

        return this.httpRequestHandler.post(url, body);
    }
}