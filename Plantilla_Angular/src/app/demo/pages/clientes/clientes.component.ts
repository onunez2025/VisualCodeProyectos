import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '../../../theme/shared/shared.module';
import { ClienteService } from '../../../services/cliente.service';
import { Cliente, ClienteQueryParameters } from '../../../models/cliente.model';
import { PagedResult, Ticket } from '../../../models/ticket.model';
import { TicketService } from '../../../services/ticket.service';
import { ProductoRegistrado } from '../../../models/producto-registrado.model';
import { ProductoRegistradoService } from '../../../services/producto-registrado.service';
import { InformeTecnicoService } from '../../../services/informe-tecnico.service';
import { InformeTecnico } from '../../../models/informe-tecnico.model';

@Component({
  selector: 'app-clientes',
  standalone: true,
  imports: [CommonModule, SharedModule, FormsModule, NgbModule],
  templateUrl: './clientes.component.html',
  styleUrl: './clientes.component.scss'
})
export class ClientesComponent implements OnInit {
  clientes: Cliente[] = [];
  totalRecords = 0;
  currentPage = 1;
  pageSize = 20;
  totalPages = 0;
  Math = Math;

  // Filtros
  filtroNombre = '';
  filtroNumeroDocumento = '';
  filtroDepartamento = '';
  filtroDistrito = '';

  // Sorting
  sortColumn: string = 'creadoEl';
  sortDirection: 'asc' | 'desc' = 'desc';

  // Cliente seleccionado para el modal
  selectedCliente: Cliente | null = null;
  clienteTickets: Ticket[] = [];
  clienteProductos: ProductoRegistrado[] = [];
  loadingTickets = false;
  loadingProductos = false;
  activeTab = 'datos'; // 'datos', 'tickets' o 'productos'
  selectedTicket: Ticket | null = null;
  selectedProducto: ProductoRegistrado | null = null;
  informeTecnico: InformeTecnico | null = null;
  loadingInforme = false;
  informeError = '';

  // Estado de carga
  isLoading = false;
  errorMessage = '';

  constructor(
    private clienteService: ClienteService,
    private ticketService: TicketService,
    private productoRegistradoService: ProductoRegistradoService,
    private informeTecnicoService: InformeTecnicoService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.loadClientes();
  }

  loadClientes(): void {
    this.isLoading = true;
    this.errorMessage = '';

    const params: ClienteQueryParameters = {
      pageNumber: this.currentPage,
      pageSize: this.pageSize,
      nombre: this.filtroNombre || undefined,
      numeroDocumento: this.filtroNumeroDocumento || undefined,
      departamento: this.filtroDepartamento || undefined,
      distrito: this.filtroDistrito || undefined
    };

    this.clienteService.getClientes(params).subscribe({
      next: (result: PagedResult<Cliente>) => {
        this.clientes = result.data;
        this.totalRecords = result.totalRecords;
        this.totalPages = result.totalPages;
        this.currentPage = result.pageNumber;
        this.isLoading = false;

        // Aplicar ordenamiento local
        this.applySorting();
      },
      error: (error) => {
        console.error('Error loading clientes:', error);
        this.errorMessage = 'Error al cargar los clientes. Por favor, intente nuevamente.';
        this.isLoading = false;
      }
    });
  }

  buscar(): void {
    this.currentPage = 1;
    this.loadClientes();
  }

  limpiarFiltros(): void {
    this.filtroNombre = '';
    this.filtroNumeroDocumento = '';
    this.filtroDepartamento = '';
    this.filtroDistrito = '';
    this.currentPage = 1;
    this.loadClientes();
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.loadClientes();
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadClientes();
    }
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadClientes();
    }
  }

  getPageNumbers(): number[] {
    const maxPagesToShow = 5;
    const pages: number[] = [];
    
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

  sortBy(column: string): void {
    if (this.sortColumn === column) {
      // Toggle direction
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }
    this.applySorting();
  }

  applySorting(): void {
    this.clientes.sort((a, b) => {
      let aValue: any;
      let bValue: any;

      switch (this.sortColumn) {
        case 'id':
          aValue = a.id || '';
          bValue = b.id || '';
          break;
        case 'nombreCompleto':
          aValue = `${a.nombre || ''} ${a.apellido || ''}`.toLowerCase();
          bValue = `${b.nombre || ''} ${b.apellido || ''}`.toLowerCase();
          break;
        case 'tipoDocumento':
          aValue = a.tipoDocumento || '';
          bValue = b.tipoDocumento || '';
          break;
        case 'numeroDocumento':
          aValue = a.numeroDocumento || '';
          bValue = b.numeroDocumento || '';
          break;
        case 'celular':
          aValue = a.celular || '';
          bValue = b.celular || '';
          break;
        case 'correoElectronico':
          aValue = a.correoElectronico || '';
          bValue = b.correoElectronico || '';
          break;
        case 'departamento':
          aValue = a.departamento || '';
          bValue = b.departamento || '';
          break;
        case 'distrito':
          aValue = a.distrito || '';
          bValue = b.distrito || '';
          break;
        case 'creadoEl':
          aValue = a.creadoEl ? new Date(a.creadoEl).getTime() : 0;
          bValue = b.creadoEl ? new Date(b.creadoEl).getTime() : 0;
          break;
        default:
          return 0;
      }

      if (aValue < bValue) {
        return this.sortDirection === 'asc' ? -1 : 1;
      }
      if (aValue > bValue) {
        return this.sortDirection === 'asc' ? 1 : -1;
      }
      return 0;
    });
  }

  getSortIcon(column: string): string {
    if (this.sortColumn !== column) {
      return 'feather icon-chevrons-up-down';
    }
    return this.sortDirection === 'asc' ? 'feather icon-chevron-up' : 'feather icon-chevron-down';
  }

  openDetailModal(content: any, cliente: Cliente): void {
    this.selectedCliente = cliente;
    this.activeTab = 'datos';
    this.clienteTickets = [];
    this.clienteProductos = [];
    this.loadClienteTickets(); // Cargar tickets inmediatamente
    this.loadClienteProductos(); // Cargar productos inmediatamente
    this.modalService.open(content, { size: 'xl', centered: true, scrollable: true });
  }

  loadClienteTickets(): void {
    if (!this.selectedCliente) return;
    
    this.loadingTickets = true;
    this.ticketService.getTickets({
      pageNumber: 1,
      pageSize: 100,
      nombreCliente: this.selectedCliente.numeroDocumento || undefined
    }).subscribe({
      next: (result) => {
        this.clienteTickets = result.data;
        this.loadingTickets = false;
      },
      error: (error) => {
        console.error('Error loading cliente tickets:', error);
        this.loadingTickets = false;
      }
    });
  }

  loadClienteProductos(): void {
    if (!this.selectedCliente) return;
    
    console.log('Loading productos for cliente:', this.selectedCliente.id);
    this.loadingProductos = true;
    this.productoRegistradoService.getProductosByCliente(this.selectedCliente.id).subscribe({
      next: (productos) => {
        console.log('Productos received:', productos);
        this.clienteProductos = productos;
        this.loadingProductos = false;
      },
      error: (error) => {
        console.error('Error loading cliente productos:', error);
        this.loadingProductos = false;
      }
    });
  }

  openTicketDetail(modalContent: any, ticket: Ticket): void {
    this.selectedTicket = ticket;
    this.informeTecnico = null;
    this.informeError = '';
    this.loadInformeTecnico();
    this.modalService.open(modalContent, { size: 'lg', centered: true, scrollable: true });
  }

  loadInformeTecnico(): void {
    if (!this.selectedTicket) return;
    
    this.loadingInforme = true;
    this.informeError = '';
    this.informeTecnicoService.getInformeByTicket(this.selectedTicket.id).subscribe({
      next: (informe) => {
        this.informeTecnico = informe;
        this.loadingInforme = false;
      },
      error: (error) => {
        if (error.status === 404) {
          this.informeError = 'No hay informe técnico para este ticket';
        } else {
          this.informeError = 'Error al cargar el informe técnico';
        }
        console.error('Error loading informe tecnico:', error);
        this.loadingInforme = false;
      }
    });
  }

  openInformeModal(informeModal: any): void {
    this.modalService.open(informeModal, { size: 'xl', centered: true, scrollable: true });
  }

  openProductoDetail(modalContent: any, producto: ProductoRegistrado): void {
    this.selectedProducto = producto;
    this.modalService.open(modalContent, { size: 'lg', centered: true, scrollable: true });
  }

  selectTab(tab: string): void {
    this.activeTab = tab;
  }

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
}
