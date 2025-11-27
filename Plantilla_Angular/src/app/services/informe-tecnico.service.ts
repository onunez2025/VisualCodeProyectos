import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InformeTecnico } from '../models/informe-tecnico.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InformeTecnicoService {
  private apiUrl = `${environment.apiUrl}/informetecnico`;

  constructor(private http: HttpClient) { }

  getInformeByTicket(ticketId: string): Observable<InformeTecnico> {
    return this.http.get<InformeTecnico>(`${this.apiUrl}/ticket/${ticketId}`);
  }

  getInformeById(id: string): Observable<InformeTecnico> {
    return this.http.get<InformeTecnico>(`${this.apiUrl}/${id}`);
  }
}
