import OrderStatus from '../DataClasses/OrderStatus';
import Order from '../DataClasses/Order';
import Heading from '../Components/Heading';
import OrderList from '../Components/OrderList';
import {getCleanerOrders, postOrder} from '../Api/endpoints';
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
    getCleanerOrders()
    .then(setOrders)
    .catch((err) => {
      console.log(err);
      setOrders([]);
    })
  });

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
          document.location.reload();
        }}
        shouldDisplayDeleteButton={(order: Order) => order.status === OrderStatus.Active}

        acceptButtonLabel='Akceptuj'
        onAcceptButtonClick={async (order: Order) => {
          const newOrder = {...order};
          newOrder.status = OrderStatus.InProgress;
          await postOrder(order.id, newOrder);
          document.location.reload();
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
          document.location.reload();
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
