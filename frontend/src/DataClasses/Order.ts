import Address from "./Address";
import DateTimeOffset from "./DateTimeOffset";
import MessLevel from "./MessLevel";
import OrderStatus from "./OrderStatus";
import Rating from "./Rating";

interface Order {
    id: number;
    clientId: string;
    cleanerId: string;
    status: OrderStatus;
    maxPrice: number;
    minRating: number;
    date: DateTimeOffset;
    messLevel: MessLevel;
    address: Address;
    opinionFromClient: Rating;
    opinionFromCleaner: Rating;
}

export default Order;