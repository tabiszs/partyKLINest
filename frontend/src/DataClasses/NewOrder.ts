import Address from "./Address";
import DateTimeOffset from "./DateTimeOffset";
import MessLevel from "./MessLevel";

interface NewOrder {
    clientId: string;
    maxPrice: number;
    minRating: number;
    messLevel: MessLevel;
    date: DateTimeOffset;
    address: Address;
}

export default NewOrder;