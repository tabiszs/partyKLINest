import DateTimeOffset from "./DateTimeOffset";
import MessLevel from "./MessLevel";
import OrderStatus from "./OrderStatus";

interface Order {
    id: number;
    clientId: string;
    cleanerId: string;
    status: OrderStatus;
    maxPrice: number;
    minRating: number;
    date: DateTimeOffset;
    messLevel: MessLevel;
}

export default Order;