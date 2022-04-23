import Order from '../../DataClasses/Order';
import MessLevel from '../../DataClasses/MessLevel';
import OrderStatus from '../../DataClasses/OrderStatus';
import { emptyAddress } from '../../DataClasses/Address';
import Heading from '../../Components/Heading';
import OrderList from '../../Components/AdminOrderList';
import { getAllOrders, deleteOrder, postOrder } from '../../Api/endpoints';
import './OrderDeletion.css';
import Dialog from '@mui/material/Dialog';
import { useEffect, useState } from 'react';
import { DialogTitle, List, ListItem, ListItemText } from '@mui/material';

const mockOrders: Order[] = [
  {
    id: 5, clientId: 'dd',
    maxPrice: 10, minRating: 5.0,
    messLevel: MessLevel.Disaster,
    status: OrderStatus.Cancelled,
    date: new Date(),
    address: emptyAddress(),
    opinionFromCleaner: { rating: 1, comment: "" },
    opinionFromClient: { rating: 1, comment: "" }
  },
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 10000, minRating: 8.0,
    messLevel: MessLevel.Moderate,
    status: OrderStatus.InProgress,
    date: new Date(),
    address: emptyAddress(),
    opinionFromCleaner: { rating: 1, comment: "" },
    opinionFromClient: { rating: 1, comment: "" }
  },
  {
    id: 5, clientId: 'dd',
    maxPrice: 500, minRating: 2.0,
    messLevel: MessLevel.Huge,
    status: OrderStatus.Active,
    date: new Date(),
    address: emptyAddress(),
    opinionFromCleaner: { rating: 1, comment: "" },
    opinionFromClient: { rating: 1, comment: "" }
  },
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 2000, minRating: 5.5,
    messLevel: MessLevel.Low,
    status: OrderStatus.Closed,
    date: new Date(),
    address: emptyAddress(),
    opinionFromCleaner: { rating: 1, comment: "" },
    opinionFromClient: { rating: 1, comment: "" }
  },
];

const mockMatching = ['aaa', 'bbb', 'ccc'];

const OrderDeletion = () => {

  const [open, setOpen] = useState(false);
  const [currentOrder, setCurrentOrder] = useState<Order | null>(null);
  const [matching, setMatching] = useState<string[]>([]);
  
  const openAssignDialog = (order: Order) => {
    setOpen(true);
    setCurrentOrder(order);
  }
  const cancelAssignDialog = () => {
    setOpen(false);
    setCurrentOrder(null);
  }
  const confirmAssignDialog = (order: Order, cleanerId: string) => {
    postOrder(order.id, { ...order, cleanerId: cleanerId });
    setOpen(false);
    setCurrentOrder(null);
  }

  useEffect(() => {
    if (currentOrder !== null)
      setMatching(mockMatching); // setMatching(getMatchingCleanerIdsForOrder(currentOrder?.id)); TODO
    else
      setMatching([]);
  }, [currentOrder])

  // TODO: zamienić gdy będzie działać API
  // const orders = getAllOrders();
  const orders = mockOrders;
  return (
    <div>
      <Heading content='Wszystkie ogłoszenia' />
      <OrderList
        orders={orders}
        deleteButtonLabel='Usuń'
        onDeleteButtonClick={async (order: Order) => { await deleteOrder(order.id) }}
        assignButtonLabel='Przyporządkuj'
        onAssignButtonClick={openAssignDialog}
      />
      <Dialog open={open} onClose={cancelAssignDialog}>
        <DialogTitle>Wybierz osobę sprzątającą do zamówienia</DialogTitle>
        <List>
          {matching.map((cleanerId) => (
            <ListItem button onClick={() => confirmAssignDialog(currentOrder!, cleanerId)} key={cleanerId}>
              <ListItemText primary={cleanerId} />
            </ListItem>
          ))}
        </List>
      </Dialog>
    </div>
  );
}

export default OrderDeletion;
