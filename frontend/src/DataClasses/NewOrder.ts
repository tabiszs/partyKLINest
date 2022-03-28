import MessLevel from "./MessLevel";

interface NewOrder {
    maxPrice: number;
    minRating: number;
    messLevel: MessLevel;
}

export default NewOrder;