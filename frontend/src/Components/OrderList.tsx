import Button from '@mui/material/Button';
import Card from '@mui/material/Card';
import Order from '../DataClasses/Order';
import {messLevelText} from '../DataClasses/MessLevel';
import {orderStatusText} from '../DataClasses/OrderStatus';
import './OrderList.css';

interface CardButtonProps {
  label: string;
  onClick: () => void;
}

const CardButton = (props: CardButtonProps) => {
  return (
    <Button
      variant='outlined'
      color='error'
      onClick={props.onClick}
    >
      {props.label}
    </Button>
  );
}

interface OrderCardProps {
  buttonLabel: string;
  onButtonClick: () => void;
  order: Order;
  displayButton: boolean;
}

const OrderCard = (props: OrderCardProps) => {
  return (
    <div className='order-card'>
      <Card variant='outlined'>
        <div className='card-content'>
          <div className='card-column'>
            Maksymalna cena: {props.order.maxPrice}zł
            <br />
            Minimalna ocena: {props.order.minRating}
            <br />
            Status: {orderStatusText(props.order.status)}
            <br />
            Poziom bałaganu: {messLevelText(props.order.messLevel)}
            <br />
            Od: {props.order.date.start.toLocaleString()}
            <br />
            Do: {props.order.date.end.toLocaleString()}
          </div>
          <div className='card-column'>
            {props.displayButton
              ? <CardButton label={props.buttonLabel} onClick={props.onButtonClick} />
              : null}
          </div>
        </div>
      </Card>
    </div>
  );
}

export interface OrderListProps {
  buttonLabel: string;
  onButtonClick: (order: Order) => void;
  orders: Order[];
  shouldDisplayButton: (order: Order) => boolean;
}

const OrderList = (props: OrderListProps) => {
  const orderCards = [];
  for (const order of props.orders) {
    orderCards.push(<OrderCard
      buttonLabel={props.buttonLabel}
      onButtonClick={() => props.onButtonClick(order)}
      displayButton={props.shouldDisplayButton(order)}
      order={order}
    />);
  }

  return (
    <div className='order-list-container'>
      {orderCards}
    </div>
  );
};

export default OrderList;
