import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductoRegistrado } from '../models/producto-registrado.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductoRegistradoService {
  private apiUrl = `${environment.apiUrl}/productosregistrados`;

  constructor(private http: HttpClient) { }

  getProductosByCliente(clienteId: string): Observable<ProductoRegistrado[]> {
    return this.http.get<ProductoRegistrado[]>(`${this.apiUrl}/cliente/${clienteId}`);
  }
}
