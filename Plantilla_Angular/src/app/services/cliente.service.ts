import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente, ClienteQueryParameters } from '../models/cliente.model';
import { PagedResult } from '../models/ticket.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  private apiUrl = `${environment.apiUrl}/clientes`;

  constructor(private http: HttpClient) { }

  getClientes(parameters: ClienteQueryParameters): Observable<PagedResult<Cliente>> {
    let params = new HttpParams();

    if (parameters.pageNumber) {
      params = params.set('pageNumber', parameters.pageNumber.toString());
    }
    if (parameters.pageSize) {
      params = params.set('pageSize', parameters.pageSize.toString());
    }
    if (parameters.nombre) {
      params = params.set('nombre', parameters.nombre);
    }
    if (parameters.numeroDocumento) {
      params = params.set('numeroDocumento', parameters.numeroDocumento);
    }
    if (parameters.departamento) {
      params = params.set('departamento', parameters.departamento);
    }
    if (parameters.distrito) {
      params = params.set('distrito', parameters.distrito);
    }

    return this.http.get<PagedResult<Cliente>>(this.apiUrl, { params });
  }

  getClienteById(id: string): Observable<Cliente> {
    return this.http.get<Cliente>(`${this.apiUrl}/${id}`);
  }
}
