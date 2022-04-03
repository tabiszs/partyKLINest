import {useState} from 'react';
import Button from '@mui/material/Button';
import Card from '@mui/material/Card';
import OrderStatus, {orderStatusText} from '../DataClasses/OrderStatus';
import MessLevel, {messLevelText} from '../DataClasses/MessLevel';
import Order from '../DataClasses/Order';
import Heading from '../Components/Heading';
import './ClientOrderManagement.css';

const mockOrders: Order[] = [
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 10, minRating: 5.0,
    messLevel: MessLevel.Disaster,
    status: OrderStatus.Cancelled,
    date: {
      start: new Date(),
      end: new Date()
    }
  },
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 10000, minRating: 8.0,
    messLevel: MessLevel.Moderate,
    status: OrderStatus.InProgress,
    date: {
      start: new Date(),
      end: new Date()
    }
  },
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 500, minRating: 2.0,
    messLevel: MessLevel.Huge,
    status: OrderStatus.Active,
    date: {
      start: new Date(),
      end: new Date()
    }
  },
  {
    id: 5, clientId: 'dd', cleanerId: 'st',
    maxPrice: 2000, minRating: 5.5,
    messLevel: MessLevel.Low,
    status: OrderStatus.Closed,
    date: {
      start: new Date(),
      end: new Date()
    }
  },
];

const CancelButton = () => {
  return (
    <Button
      variant='outlined'
      color='error'
      onClick={() => alert('cancel')}
    >
      Anuluj
    </Button>
  );
}

const OrderCard = (props: Order) => {
  return (
    <div className='order-card'>
      <Card variant='outlined'>
        <div className='card-content'>
          <div className='card-column'>
            Maksymalna cena: {props.maxPrice}zł
            <br />
            Minimalna ocena: {props.minRating}
            <br />
            Status: {orderStatusText(props.status)}
            <br />
            Poziom bałaganu: {messLevelText(props.messLevel)}
          </div>
          <div className='card-column'>
            {props.status === OrderStatus.Active
              ? <CancelButton />
              : null}
          </div>
        </div>
      </Card>
    </div>
  );
}

interface OrderListProps {
  orders: Order[];
}

const OrderList = (props: OrderListProps) => {
  const orderCards = [];

  for (const order of props.orders) {
    orderCards.push(<OrderCard {...order} />)
  }

  return (
    <div>
      {orderCards}
    </div>
  );
}

const OrderManagement = () => {
  return (
    <div className='order-management-screen'>
      <Heading content='Twoje ogłoszenia' />
      <OrderList orders={mockOrders} />
    </div>
  );
};

export default OrderManagement;
