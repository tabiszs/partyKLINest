enum OrderStatus {
  Active = "Active",
  InProgress = "InProgress",
  Cancelled = "Cancelled",
  Closed = "Closed"
}

export function orderStatusText(orderStatus: OrderStatus): string {
  switch (orderStatus) {
    case OrderStatus.Active: return 'Aktywne';
    case OrderStatus.InProgress: return 'W trakcie';
    case OrderStatus.Cancelled: return 'Anulowane';
    case OrderStatus.Closed: return 'Zakończone';
    default: return 'Nieznany';
  }
}

export default OrderStatus;
