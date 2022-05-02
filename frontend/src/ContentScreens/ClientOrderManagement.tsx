import OrderStatus from '../DataClasses/OrderStatus';
import MessLevel from '../DataClasses/MessLevel';
import Order from '../DataClasses/Order';
import Heading from '../Components/Heading';
import OrderList from '../Components/OrderList';
import {deleteOrder, getClientOrders} from '../Api/endpoints';
import './ClientOrderManagement.css';
import {emptyAddress} from '../DataClasses/Address';
import { useState } from 'react';
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
  // const orders = getClientOrders();
  const orders = mockOrders;

  const [open, setOpen] = useState(false);
  const [rating, setRating] = useState<Rating>({rating:0,comment:""});
  const [userType, setUserType] = useState<UserType>(UserType.Cleaner);

  const [ratingOpen, setRatingOpen] = useState(false);
  const [order, setOrder] = useState<Order | undefined>(undefined);

  const showRatingPopup = (rating: Rating, userType: UserType) => {
    setRating(rating);
    setUserType(userType);
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
        deleteButtonLabel='Anuluj'
        onDeleteButtonClick={async (order: Order) => {await deleteOrder(order.id)}}
        shouldDisplayDeleteButton={(order: Order) => order.status === OrderStatus.Active}
        showRatingPopup={showRatingPopup}
        opinionButtonLabel='Oceń'
        onOpinionButtonClick={showRateOrder}
        shouldDisplayOpinionButton={(order: Order) => order.opinionFromClient === undefined && order.status === OrderStatus.Closed}
      />
      <OpinionPopup opinion={rating} open={open} userType={userType} cancelDialog={() => setOpen(false)} />
      <RateOrder order={order} userType={UserType.Client} open={ratingOpen} closeDialog={() => setRatingOpen(false)} />
    </div>
  );
};

export default OrderManagement;
