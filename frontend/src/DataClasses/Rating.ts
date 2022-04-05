interface Rating {
    rating: number;
    comment: string;
}

export const isRatingCorrect = (rating: Rating) => 
    rating.rating >= 1 && rating.rating <= 10;

export default Rating;