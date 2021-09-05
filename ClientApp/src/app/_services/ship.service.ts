import { Injectable } from '@angular/core';
import { Observable, throwError, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Ship } from '../_models/ship';
import 'rxjs/Rx';
import { ShipResponse } from '../_models/shipresponse';
import { environment } from '../../environments/environment';


const baseUrl = `${environment.apiUrl}/ships`;

@Injectable({
  providedIn: 'root'
})
export class ShipService {

  constructor(private http: HttpClient) { }

  getAllShips(): Observable<ShipResponse> {
    return this.http.get<ShipResponse>(baseUrl + '/getAll');
  }

  getShipByCode(code: string): Observable<ShipResponse> {
    return this.http.get(baseUrl + '/getShipByCode/' + code);
  }

  create(data: any): Observable<ShipResponse> {
    return this.http.post(baseUrl + '/create', data);
  }

  update(data: any): Observable<ShipResponse> {
    return this.http.put(baseUrl + '/update/', data)
  }

  delete(code: string): Observable<ShipResponse> {
    return this.http.delete(baseUrl + '/delete/' + code);
  }
  isCodeUnique(code: string): Observable<ShipResponse> {
    return this.http.get(baseUrl + '/uniquecode/' + name);

  }

  isNameUnique(name: string): Observable<ShipResponse> {
    return this.http.get(baseUrl + '/uniquename/' + name);

  }

}

