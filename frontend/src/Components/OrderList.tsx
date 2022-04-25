import Button from '@mui/material/Button';
import Card from '@mui/material/Card';
import Order from '../DataClasses/Order';
import {messLevelText} from '../DataClasses/MessLevel';
import {orderStatusText} from '../DataClasses/OrderStatus';
import './OrderList.css';
import Rating from '../DataClasses/Rating';
import Link from '@mui/material/Link';
import UserType from '../DataClasses/UserType';

interface CardButtonProps {
  label: string;
  onClick: () => void;
  color: "inherit" | "primary" | "secondary" | "success" | "error" | "info" | "warning" | undefined;
}

const CardButton = (props: CardButtonProps) => {
  return (
    <Button
      variant='outlined'
      color={props.color}
      onClick={props.onClick}
    >
      {props.label}
    </Button>
  );
}

interface OrderCardProps {
  deleteButtonLabel: string;
  opinionButtonLabel: string;
  onDeleteButtonClick: () => void;
  onOpinionButtonClick: () => void;
  showRatingPopup: (opinion: Rating, userType: UserType) => void;
  order: Order;
  displayDeleteButton: boolean;
  displayOpinionButton: boolean;
}

const OrderCard = (props: OrderCardProps) => {

  const getOpinionSummary = (userType: UserType, opinion?: Rating) => {
    if (opinion === undefined) return <>Nie wystawiono</>;

    return (
      <>
        {opinion.rating}{"/10 "}
        <Link component="button" onClick={() => props.showRatingPopup(opinion, userType)}>Szczegóły</Link>
      </>
    );
  }

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
            <span className='opinion-summary'>
            Opinia klienta: {getOpinionSummary(UserType.Client, props.order.opinionFromClient)}
            </span><span className='opinion-summary'>
            Opinia sprzątającego: {getOpinionSummary(UserType.Cleaner, props.order.opinionFromCleaner)}
            </span>
          </div>
          <div className='card-column'>
            {props.displayOpinionButton
              ? <CardButton label={props.opinionButtonLabel} onClick={props.onOpinionButtonClick} color="primary"/>
              : null}
            {props.displayDeleteButton
              ? <CardButton label={props.deleteButtonLabel} onClick={props.onDeleteButtonClick} color="error"/>
              : null}
          </div>
        </div>
      </Card>
    </div>
  );
}

export interface OrderListProps {
  deleteButtonLabel: string;
  opinionButtonLabel: string;
  onDeleteButtonClick: (order: Order) => void;
  onOpinionButtonClick: (order: Order) => void;
  orders: Order[];
  shouldDisplayDeleteButton: (order: Order) => boolean;
  shouldDisplayOpinionButton: (order: Order) => boolean;
  showRatingPopup: (rating: Rating, userType: UserType) => void;
}

const OrderList = (props: OrderListProps) => {
  const orderCards = [];
  for (const order of props.orders) {
    orderCards.push(<OrderCard
      deleteButtonLabel={props.deleteButtonLabel}
      opinionButtonLabel={props.opinionButtonLabel}
      onDeleteButtonClick={() => props.onDeleteButtonClick(order)}
      onOpinionButtonClick={() => props.onOpinionButtonClick(order)}
      displayDeleteButton={props.shouldDisplayDeleteButton(order)}
      displayOpinionButton={props.shouldDisplayOpinionButton(order)}
      order={order}
      showRatingPopup={props.showRatingPopup}
    />);
  }

  return (
    <div className='order-list-container'>
      {orderCards}
    </div>
  );
};

export default OrderList;
