// angular import
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

// project import
import { SharedModule } from 'src/app/theme/shared/shared.module';
import { DashboardService, DashboardStats } from 'src/app/services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, SharedModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  stats: DashboardStats | null = null;
  loading = true;

  constructor(private dashboardService: DashboardService) {}

  // life cycle event
  ngOnInit() {
    this.loadDashboardData();
  }

  loadDashboardData() {
    this.loading = true;
    this.dashboardService.getDashboardStats().subscribe({
      next: (data) => {
        this.stats = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error cargando estadísticas:', error);
        this.loading = false;
      }
    });
  }

  getEstadoColor(estado: string): string {
    if (estado.includes('Cerrado') || estado.includes('Completado')) return 'text-c-green';
    if (estado.includes('Pendiente')) return 'text-c-yellow';
    if (estado.includes('Proceso') || estado.includes('ruta')) return 'text-c-blue';
    return 'text-muted';
  }

  getEstadoIcon(estado: string): string {
    if (estado.includes('Cerrado') || estado.includes('Completado')) return 'icon-check-circle';
    if (estado.includes('Pendiente')) return 'icon-clock';
    if (estado.includes('Proceso') || estado.includes('ruta')) return 'icon-truck';
    return 'icon-info';
  }

  formatFecha(fecha: string | null | undefined): string {
    if (!fecha) return '-';
    const date = new Date(fecha);
    return date.toLocaleDateString('es-PE', { day: '2-digit', month: 'short' });
  }
}
