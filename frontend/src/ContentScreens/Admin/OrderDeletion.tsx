import Order from '../../DataClasses/Order';
import MessLevel from '../../DataClasses/MessLevel';
import OrderStatus from '../../DataClasses/OrderStatus';
import {emptyAddress} from '../../DataClasses/Address';
import Heading from '../../Components/Heading';
import OrderList from '../../Components/OrderList';
import {getAllOrders, deleteOrder} from '../../Api/endpoints';
import './OrderDeletion.css';

const mockOrders: Order[] = [
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 10, minRating: 5.0,
    messLevel: MessLevel.Disaster,
    status: OrderStatus.Cancelled,
    date: new Date(),
    address: emptyAddress(),
    opinionFromCleaner: {rating: 1, comment: ""},
    opinionFromClient: {rating: 1, comment: ""}
  },
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 10000, minRating: 8.0,
    messLevel: MessLevel.Moderate,
    status: OrderStatus.InProgress,
    date: new Date(),
    address: emptyAddress(),
    opinionFromCleaner: {rating: 1, comment: ""},
    opinionFromClient: {rating: 1, comment: ""}
  },
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 500, minRating: 2.0,
    messLevel: MessLevel.Huge,
    status: OrderStatus.Active,
    date: new Date(),
    address: emptyAddress(),
    opinionFromCleaner: {rating: 1, comment: ""},
    opinionFromClient: {rating: 1, comment: ""}
  },
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 2000, minRating: 5.5,
    messLevel: MessLevel.Low,
    status: OrderStatus.Closed,
    date: new Date(),
    address: emptyAddress(),
    opinionFromCleaner: {rating: 1, comment: ""},
    opinionFromClient: {rating: 1, comment: ""}
  },
];

const OrderDeletion = () => {
  // TODO: zamienić gdy będzie działać API
  // const orders = getAllOrders();
  const orders = mockOrders;
  return (
    <div>
      <Heading content='Wszystkie ogłoszenia' />
      <OrderList
        orders={orders}
        buttonLabel='Usuń'
        onButtonClick={async (order: Order) => {await deleteOrder(order.id)}}
        shouldDisplayButton={(_: Order) => true}
      />
    </div>
  );
}

export default OrderDeletion;
