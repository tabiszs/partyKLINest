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
  deleteButtonLabel: string;
  assignButtonLabel: string;
  onDeleteButtonClick: () => void;
  onAssignButtonClick: () => void;
  order: Order;
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
            Data: {props.order.date.toLocaleString()}
            <br />
            {props.order.cleanerId ? "ID sprzątającego: " + props.order.cleanerId! : null}
          </div>
          <div className='card-column'>
            <CardButton label={props.deleteButtonLabel} onClick={props.onDeleteButtonClick} />
            {
              props.order.cleanerId ? null
              : <CardButton label={props.assignButtonLabel} onClick={props.onAssignButtonClick} />
            }
          </div>
        </div>
      </Card>
    </div>
  );
}

export interface OrderListProps {
  deleteButtonLabel: string;
  assignButtonLabel: string;
  onDeleteButtonClick: (order: Order) => void;
  onAssignButtonClick: (order: Order) => void;
  orders: Order[];
}

const OrderList = (props: OrderListProps) => {
  const orderCards = [];
  for (const order of props.orders) {
    orderCards.push(<OrderCard
      deleteButtonLabel={props.deleteButtonLabel}
      assignButtonLabel={props.assignButtonLabel}
      onDeleteButtonClick={() => props.onDeleteButtonClick(order)}
      onAssignButtonClick={() => props.onAssignButtonClick(order)}
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
