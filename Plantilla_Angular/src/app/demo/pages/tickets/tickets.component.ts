import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from 'src/app/theme/shared/shared.module';
import { TicketService } from 'src/app/services/ticket.service';
import { Ticket, TicketQueryParameters } from 'src/app/models/ticket.model';
import { InformeTecnicoService } from 'src/app/services/informe-tecnico.service';
import { InformeTecnico } from 'src/app/models/informe-tecnico.model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-tickets',
  standalone: true,
  imports: [CommonModule, SharedModule, FormsModule, NgbModule],
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.scss']
})
export class TicketsComponent implements OnInit {
  tickets: Ticket[] = [];
  loading = false;
  error: string | null = null;
  selectedTicket: Ticket | null = null;
  Math = Math; // Exponer Math para usarlo en el template

  // Paginación
  currentPage = 1;
  pageSize = 20;
  totalRecords = 0;
  totalPages = 0;

  // Filtros
  filterNumeroTicket = '';
  filterCliente = '';
  filterProducto = '';
  filterFechaDesde = '';
  filterFechaHasta = '';

  // Ordenamiento
  sortColumn: string = 'fechaCreacion';
  sortDirection: 'asc' | 'desc' = 'desc';

  // Informe Técnico
  informeTecnico: InformeTecnico | null = null;
  loadingInforme = false;
  informeError: string | null = null;

  constructor(
    private ticketService: TicketService,
    private informeTecnicoService: InformeTecnicoService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets(): void {
    this.loading = true;
    this.error = null;

    const params: TicketQueryParameters = {
      pageNumber: this.currentPage,
      pageSize: this.pageSize,
      numeroTicket: this.filterNumeroTicket || undefined,
      nombreCliente: this.filterCliente || undefined,
      nombreProducto: this.filterProducto || undefined,
      fechaVisitaDesde: this.filterFechaDesde || undefined,
      fechaVisitaHasta: this.filterFechaHasta || undefined
    };

    this.ticketService.getTickets(params).subscribe({
      next: (response) => {
        this.tickets = response.data;
        this.totalRecords = response.totalRecords;
        this.totalPages = response.totalPages;
        this.currentPage = response.pageNumber;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error al cargar los tickets. Por favor, intente nuevamente.';
        console.error('Error:', err);
        this.loading = false;
      }
    });
  }

  // Métodos de paginación
  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadTickets();
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadTickets();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.loadTickets();
    }
  }

  // Método para aplicar filtros
  applyFilters(): void {
    this.currentPage = 1; // Resetear a la primera página
    this.loadTickets();
  }

  // Método para limpiar filtros
  clearFilters(): void {
    this.filterNumeroTicket = '';
    this.filterCliente = '';
    this.filterProducto = '';
    this.filterFechaDesde = '';
    this.filterFechaHasta = '';
    this.currentPage = 1;
    this.loadTickets();
  }

  // Generar array de páginas para paginación
  getPages(): number[] {
    const pages: number[] = [];
    const maxPagesToShow = 5;
    let startPage = Math.max(1, this.currentPage - Math.floor(maxPagesToShow / 2));
    let endPage = Math.min(this.totalPages, startPage + maxPagesToShow - 1);

    if (endPage - startPage < maxPagesToShow - 1) {
      startPage = Math.max(1, endPage - maxPagesToShow + 1);
    }

    for (let i = startPage; i <= endPage; i++) {
      pages.push(i);
    }

    return pages;
  }

  // Formatear fecha para mostrar
  formatDate(dateString: string | null): string {
    if (!dateString) return '-';
    const date = new Date(dateString);
    return date.toLocaleDateString('es-PE');
  }

  // Formatear fecha y hora
  formatDateTime(dateString: string | null): string {
    if (!dateString) return '-';
    const date = new Date(dateString);
    return date.toLocaleDateString('es-PE') + ' ' + date.toLocaleTimeString('es-PE', { hour: '2-digit', minute: '2-digit' });
  }

  // Abrir modal de detalle
  openDetail(ticket: Ticket, content: any): void {
    console.log('Opening detail for ticket:', ticket.id);
    this.selectedTicket = ticket;
    try {
      this.modalService.open(content, { 
        size: 'xl',
        centered: true,
        scrollable: true
      });
    } catch (error) {
      console.error('Error opening modal:', error);
      alert('Error al abrir el detalle del ticket');
    }
  }

  // Cerrar modal
  closeModal(): void {
    this.selectedTicket = null;
    this.modalService.dismissAll();
  }

  // Obtener clase de badge según estado
  getEstadoBadgeClass(estado: string | null): string {
    if (!estado) return 'bg-secondary text-white';
    
    const estadoLower = estado.toLowerCase();
    if (estadoLower.includes('atendido') || estadoLower.includes('completado') || estadoLower.includes('cerrado')) {
      return 'bg-success text-white';
    } else if (estadoLower.includes('proceso') || estadoLower.includes('asignado') || estadoLower.includes('aceptado')) {
      return 'bg-info text-white';
    } else if (estadoLower.includes('pendiente')) {
      return 'bg-warning text-dark';
    } else if (estadoLower.includes('cancelado') || estadoLower.includes('rechazado')) {
      return 'bg-danger text-white';
    }
    return 'bg-secondary text-white';
  }

  // Ordenar por columna
  sortBy(column: string): void {
    if (this.sortColumn === column) {
      // Cambiar dirección si es la misma columna
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      // Nueva columna, ordenar ascendente por defecto
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }
    
    // Ordenar tickets localmente
    this.tickets.sort((a, b) => {
      let valueA: any;
      let valueB: any;

      switch(column) {
        case 'id':
          valueA = a.id || '';
          valueB = b.id || '';
          break;
        case 'cliente':
          valueA = a.clienteLabel || a.clienteNombreCompleto || '';
          valueB = b.clienteLabel || b.clienteNombreCompleto || '';
          break;
        case 'productos':
          valueA = a.productosRegistrados || '';
          valueB = b.productosRegistrados || '';
          break;
        case 'fechaVisita':
          valueA = a.fechaVisita ? new Date(a.fechaVisita).getTime() : 0;
          valueB = b.fechaVisita ? new Date(b.fechaVisita).getTime() : 0;
          break;
        case 'estado':
          valueA = a.estado || '';
          valueB = b.estado || '';
          break;
        case 'tipoServicio':
          valueA = a.tipoServicio || '';
          valueB = b.tipoServicio || '';
          break;
        case 'tecnico':
          valueA = a.tecnico || '';
          valueB = b.tecnico || '';
          break;
        case 'distrito':
          valueA = a.distrito || '';
          valueB = b.distrito || '';
          break;
        case 'fechaCreacion':
          valueA = a.creadoEl ? new Date(a.creadoEl).getTime() : 0;
          valueB = b.creadoEl ? new Date(b.creadoEl).getTime() : 0;
          break;
        default:
          return 0;
      }

      if (valueA < valueB) {
        return this.sortDirection === 'asc' ? -1 : 1;
      }
      if (valueA > valueB) {
        return this.sortDirection === 'asc' ? 1 : -1;
      }
      return 0;
    });
  }

  // Obtener icono de ordenamiento
  getSortIcon(column: string): string {
    if (this.sortColumn !== column) {
      return 'feather icon-chevrons-up-down';
    }
    return this.sortDirection === 'asc' ? 'feather icon-chevron-up' : 'feather icon-chevron-down';
  }

  // Abrir modal de informe técnico directamente desde la tabla
  openInformeModal(ticket: Ticket, modal: any): void {
    this.selectedTicket = ticket;
    this.informeTecnico = null;
    this.loadingInforme = true;
    this.informeError = null;
    
    this.informeTecnicoService.getInformeByTicket(ticket.id).subscribe({
      next: (informe) => {
        this.informeTecnico = informe;
        this.loadingInforme = false;
        this.modalService.open(modal, { size: 'lg', backdrop: 'static' });
      },
      error: (err) => {
        this.loadingInforme = false;
        this.informeError = 'Error al cargar informe técnico';
        console.error('Error al cargar informe:', err);
      }
    });
  }

  // Métodos para manejo de imágenes
  getImageUrl(imageUrl: string): string {
    if (!imageUrl) return '';
    
    // Si ya es una URL completa, retornarla
    if (imageUrl.startsWith('http://') || imageUrl.startsWith('https://')) {
      return imageUrl;
    }
    
    // Si es una ruta relativa, construir URL completa
    const baseUrl = environment.apiUrl.replace('/api', '');
    return `${baseUrl}${imageUrl}`;
  }

  isImageFile(fileName: string | null | undefined): boolean {
    if (!fileName) return false;
    const imageExtensions = ['.jpg', '.jpeg', '.png', '.gif', '.bmp', '.webp'];
    const lowerFileName = fileName.toLowerCase();
    return imageExtensions.some(ext => lowerFileName.endsWith(ext));
  }

  isPdfFile(fileName: string | null | undefined): boolean {
    if (!fileName) return false;
    return fileName.toLowerCase().endsWith('.pdf');
  }

  getFileName(filePath: string | null | undefined): string {
    if (!filePath) return '';
    const parts = filePath.split('/');
    return parts[parts.length - 1];
  }

  onImageError(event: Event): void {
    const img = event.target as HTMLImageElement;
    img.src = 'assets/images/placeholder.svg';
    img.alt = 'Imagen no disponible';
  }
}
