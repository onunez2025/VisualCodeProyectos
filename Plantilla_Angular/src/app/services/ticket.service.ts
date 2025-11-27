import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ticket, TicketQueryParameters, PagedResult } from '../models/ticket.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private apiUrl = `${environment.apiUrl}/tickets`;

  constructor(private http: HttpClient) { }

  getTickets(params: TicketQueryParameters): Observable<PagedResult<Ticket>> {
    let httpParams = new HttpParams();

    if (params.pageNumber) {
      httpParams = httpParams.set('pageNumber', params.pageNumber.toString());
    }
    if (params.pageSize) {
      httpParams = httpParams.set('pageSize', params.pageSize.toString());
    }
    if (params.numeroTicket) {
      httpParams = httpParams.set('numeroTicket', params.numeroTicket);
    }
    if (params.nombreCliente) {
      httpParams = httpParams.set('nombreCliente', params.nombreCliente);
    }
    if (params.nombreProducto) {
      httpParams = httpParams.set('nombreProducto', params.nombreProducto);
    }
    if (params.fechaVisitaDesde) {
      httpParams = httpParams.set('fechaVisitaDesde', params.fechaVisitaDesde);
    }
    if (params.fechaVisitaHasta) {
      httpParams = httpParams.set('fechaVisitaHasta', params.fechaVisitaHasta);
    }

    return this.http.get<PagedResult<Ticket>>(this.apiUrl, { params: httpParams });
  }

  getTicketById(id: number): Observable<Ticket> {
    return this.http.get<Ticket>(`${this.apiUrl}/${id}`);
  }
}
