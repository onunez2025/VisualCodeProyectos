import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface TicketResumen {
  id: string;
  titulo: string;
  estado: string;
  fechaCreacion: string;
  cliente?: string;
}

export interface DashboardStats {
  totalTickets: number;
  ticketsPendientes: number;
  ticketsEnProceso: number;
  ticketsCerrados: number;
  ticketsDelMes: number;
  ticketsDelDia: number;
  promedioTiempoResolucion: number;
  ticketsPorEstado: { estado: string; cantidad: number }[];
  ticketsPorTipoServicio: { tipo: string; cantidad: number }[];
  ticketsPorDepartamento: { departamento: string; cantidad: number }[];
  ticketsPorEmpresa: { empresa: string; cantidad: number }[];
  ticketsPorTecnico: { tecnico: string; cantidad: number }[];
  tendenciaSemanal: { dia: string; cantidad: number }[];
  ultimosTickets: TicketResumen[];
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getDashboardStats(): Observable<DashboardStats> {
    return this.http.get<DashboardStats>(`${this.apiUrl}/dashboard/stats`);
  }
}
