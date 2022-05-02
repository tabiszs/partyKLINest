import Address from "./Address";
import MessLevel from "./MessLevel";

interface NewOrder {
    clientId: string;
    maxPrice: number;
    minRating: number;
    messLevel: MessLevel;
    date: Date;
    address: Address;
}

export default NewOrder;
