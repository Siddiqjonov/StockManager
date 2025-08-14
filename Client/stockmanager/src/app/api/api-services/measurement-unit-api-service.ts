import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { MeasurementUnitCreateDto } from "../interfaces/create-interfaces/measurement-unit-create-dto";

@Injectable({ providedIn: 'root' })
export class MeasurementUnitApiService {
    private readonly apiUrl = 'https://localhost:7277/api/measurementUnit';

    constructor(private http: HttpClient) {}

    public create(dto: MeasurementUnitCreateDto): Observable<void> {
        return this.http.post<void>(`${this.apiUrl}/create`, dto);
    }
}
