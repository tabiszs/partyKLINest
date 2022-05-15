import Order from '../../DataClasses/Order';
import Heading from '../../Components/Heading';
import OrderList from '../../Components/AdminOrderList';
import { getAllOrders, deleteOrder, postOrder, getMatchingCleanerIdsForOrder } from '../../Api/endpoints';
import './OrderDeletion.css';
import Dialog from '@mui/material/Dialog';
import { useEffect, useState } from 'react';
import { DialogTitle, List, ListItem, ListItemText } from '@mui/material';

const OrderDeletion = () => {

  const [open, setOpen] = useState(false);
  const [currentOrder, setCurrentOrder] = useState<Order | null>(null);
  const [orders, setOrders] = useState<Order[]>([]);
  const [matching, setMatching] = useState<string[]>([]);
  
  const openAssignDialog = (order: Order) => {
    setOpen(true);
    setCurrentOrder(order);
  }
  const cancelAssignDialog = () => {
    setOpen(false);
    setCurrentOrder(null);
  }
  const confirmAssignDialog = async (order: Order, cleanerId: string) => {
    await postOrder(order.id, { ...order, cleanerId: cleanerId });
    setOpen(false);
    setCurrentOrder(null);
    document.location.reload();
  }

  useEffect(() => {
    if (currentOrder !== null)
      getMatchingCleanerIdsForOrder(currentOrder.id)
      .then(setMatching)
      .catch((err) => {
        console.log(err);
        setMatching([]);
      });
    else
      setMatching([]);
  }, [currentOrder])

  useEffect(() => {
    getAllOrders()
    .then(setOrders)
    .catch((err) => {
      console.log(err);
      setOrders([]);
    });
  }, []);

  return (
    <div>
      <Heading content='Wszystkie ogłoszenia' />
      <OrderList
        orders={orders}
        deleteButtonLabel='Usuń'
        onDeleteButtonClick={async (order: Order) => {
          await deleteOrder(order.id);
          document.location.reload();
        }}
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
