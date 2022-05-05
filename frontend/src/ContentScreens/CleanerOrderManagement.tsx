import OrderStatus from '../DataClasses/OrderStatus';
import MessLevel from '../DataClasses/MessLevel';
import Order from '../DataClasses/Order';
import Heading from '../Components/Heading';
import OrderList from '../Components/OrderList';
import {getCleanerOrders, postOrder} from '../Api/endpoints';
import './ClientOrderManagement.css';
import {emptyAddress} from '../DataClasses/Address';
import {useState} from 'react';
import Rating from '../DataClasses/Rating';
import OpinionPopup from '../Components/OpinionPopup';
import UserType from '../DataClasses/UserType';
import RateOrder from './RateOrder';

const mockOrders: Order[] = [
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 10, minRating: 5.0,
    messLevel: MessLevel.Disaster,
    status: OrderStatus.Cancelled,
    date: new Date(),
    address: emptyAddress(),
    opinionFromCleaner: {rating: 1, comment: "abcd"},
    opinionFromClient: {rating: 1, comment: "dcBA"}
  },
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 10000, minRating: 8.0,
    messLevel: MessLevel.Moderate,
    status: OrderStatus.InProgress,
    date: new Date(),
    address: emptyAddress(),
    opinionFromClient: {rating: 2, comment: "qwertyuiop"}
  },
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 500, minRating: 2.0,
    messLevel: MessLevel.Huge,
    status: OrderStatus.Active,
    date: new Date(),
    address: emptyAddress(),
    opinionFromCleaner: {rating: 3, comment: "12345"}
  },
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 2000, minRating: 5.5,
    messLevel: MessLevel.Low,
    status: OrderStatus.Closed,
    date: new Date(),
    address: emptyAddress(),
  },
];

const OrderManagement = () => {
  // TODO: gdy będzie gotowe API to zamienić
  // const orders = getCleanerOrders();
  const orders = mockOrders;

  const [open, setOpen] = useState(false);
  const [rating, setRating] = useState<Rating>({rating: 0, comment: ""});

  const [ratingOpen, setRatingOpen] = useState(false);
  const [order, setOrder] = useState<Order | undefined>(undefined);

  const showRatingPopup = (rating: Rating) => {
    setRating(rating);
    setOpen(true);
  }

  const showRateOrder = (order: Order) => {
    setOrder(order);
    setRatingOpen(true);
  }

  return (
    <div className='order-management-screen'>
      <Heading content='Twoje ogłoszenia' />
      <OrderList
        orders={orders}

        deleteButtonLabel='Odrzuć'
        onDeleteButtonClick={async (order: Order) => {
          const newOrder = {...order};
          newOrder.cleanerId = undefined;
          await postOrder(order.id, newOrder);
        }}
        shouldDisplayDeleteButton={(order: Order) => order.status === OrderStatus.Active}

        acceptButtonLabel='Akceptuj'
        onAcceptButtonClick={async (order: Order) => {
          const newOrder = {...order};
          newOrder.status = OrderStatus.InProgress;
          await postOrder(order.id, newOrder);
        }}
        shouldDisplayAcceptButton={(order: Order) => order.status === OrderStatus.Active}

        showRatingPopup={showRatingPopup}

        opinionButtonLabel='Oceń'
        onOpinionButtonClick={showRateOrder}
        shouldDisplayOpinionButton={(order: Order) => order.opinionFromCleaner === undefined && order.status === OrderStatus.Closed}

        closeButtonLabel='Wykonane'
        onCloseButtonClick={async (order: Order) => {
          const newOrder = {...order};
          newOrder.status = OrderStatus.Closed;
          await postOrder(order.id, newOrder);
        }}
        shouldDisplayCloseButton={(order: Order) => order.status === OrderStatus.InProgress}
      />

      <OpinionPopup
        opinion={rating}
        open={open}
        userType={UserType.Cleaner}
        cancelDialog={() => setOpen(false)}
      />
      <RateOrder
        order={order}
        userType={UserType.Cleaner}
        open={ratingOpen}
        closeDialog={() => setRatingOpen(false)}
      />
    </div>
  );
};

export default OrderManagement;
