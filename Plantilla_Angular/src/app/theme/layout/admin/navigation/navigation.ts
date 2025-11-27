export interface NavigationItem {
  id: string;
  title: string;
  type: 'item' | 'collapse' | 'group';
  translate?: string;
  icon?: string;
  hidden?: boolean;
  url?: string;
  classes?: string;
  exactMatch?: boolean;
  external?: boolean;
  target?: boolean;
  breadcrumbs?: boolean;

  children?: NavigationItem[];
}
export const NavigationItems: NavigationItem[] = [
  {
    id: 'navigation',
    title: 'Navigation',
    type: 'group',
    icon: 'icon-navigation',
    children: [
      {
        id: 'dashboard',
        title: 'Dashboard',
        type: 'item',
        url: '/dashboard',
        icon: 'feather icon-home',
        classes: 'nav-item'
      },
      {
        id: 'tickets',
        title: 'Tickets',
        type: 'item',
        url: '/tickets',
        icon: 'feather icon-clipboard',
        classes: 'nav-item'
      },
      {
        id: 'clientes',
        title: 'Clientes',
        type: 'item',
        url: '/clientes',
        icon: 'feather icon-users',
        classes: 'nav-item'
      }
    ]
  }
];
