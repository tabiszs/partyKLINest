import OrderStatus from '../DataClasses/OrderStatus';
import Order from '../DataClasses/Order';
import Heading from '../Components/Heading';
import OrderList from '../Components/OrderList';
import {deleteOrder, getClientOrders} from '../Api/endpoints';
import './ClientOrderManagement.css';
import {useEffect, useState} from 'react';
import Rating from '../DataClasses/Rating';
import OpinionPopup from '../Components/OpinionPopup';
import UserType from '../DataClasses/UserType';
import RateOrder from './RateOrder';

const OrderManagement = () => {

  const [orders, setOrders] = useState<Order[]>([]);

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

  useEffect(() => {
    getClientOrders()
    .then(setOrders)
    .catch((err) => {
      console.log(err);
      setOrders([]);
    });
    }, []);

  return (
    <div className='order-management-screen'>
      <Heading content='Twoje ogłoszenia' />
      <OrderList
        orders={orders}

        deleteButtonLabel='Anuluj'
        onDeleteButtonClick={async (order: Order) => {
          await deleteOrder(order.id);
          document.location.reload();
        }}
        shouldDisplayDeleteButton={(order: Order) => order.status === OrderStatus.Active}

        showRatingPopup={showRatingPopup}

        opinionButtonLabel='Oceń'
        onOpinionButtonClick={showRateOrder}
        shouldDisplayOpinionButton={(order: Order) => order.opinionFromClient === undefined && order.status === OrderStatus.Closed}

        acceptButtonLabel=''
        onAcceptButtonClick={(_) => {}}
        shouldDisplayAcceptButton={(_) => false}

        closeButtonLabel=''
        onCloseButtonClick={(_) => {}}
        shouldDisplayCloseButton={(_) => false}
      />
      <OpinionPopup opinion={rating} open={open} userType={UserType.Client} cancelDialog={() => setOpen(false)} />
      <RateOrder order={order} userType={UserType.Client} open={ratingOpen} closeDialog={() => setRatingOpen(false)} />
    </div>
  );
};

export default OrderManagement;
