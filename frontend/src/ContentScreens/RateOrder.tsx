import Button from "@mui/material/Button";
import Rating from "@mui/material/Rating";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import { useState } from "react";
import Order from "../DataClasses/Order";
import RatingStructure from "../DataClasses/Rating";
import "./RateOrder.css";
import { postOrderRate } from "../Api/endpoints";
import UserType from "../DataClasses/UserType";
import Dialog from "@mui/material/Dialog";
import DialogTitle from "@mui/material/DialogTitle";
import { DialogActions, DialogContent } from "@mui/material";

interface RateOrderProps {
    userType: UserType;
    order?: Order;
    open: boolean;
    closeDialog: () => void;
}

const RateOrder = (props: RateOrderProps) => {

    const opinion = props.userType === UserType.Cleaner ? props.order?.opinionFromCleaner : props.order?.opinionFromClient;

    const [rating, setRating] = useState(opinion ? opinion.rating : 5);
    const [comment, setComment] = useState(opinion ? opinion.comment : "");

    const sendButtonHandler = () => {
        const ratingStructure: RatingStructure = { rating: rating, comment: comment };
        console.log(ratingStructure);
        if (props.order) postOrderRate(props.order.id, ratingStructure);
        setRating(5);
        setComment("");
        props.closeDialog();
    }

    return (
        <Dialog open={props.open} onClose={props.closeDialog} fullWidth={true} maxWidth='sm'>
            <DialogTitle>Oceń zlecenie</DialogTitle>
            <DialogContent>
                <div className="rateorder-container">
                    <div className="field-container">
                        <Typography component="legend">Ocena</Typography>
                        <Rating
                            value={rating / 2}
                            precision={0.5}
                            onChange={(_, value) => value != null && setRating(value * 2)}
                        />
                    </div>
                    <div className="field-container">
                        <TextField
                            className="full-width"
                            value={comment}
                            onChange={(e) => setComment(e.target.value)}
                            variant="outlined"
                            label="Komentarz"
                            multiline
                        />
                    </div>
                </div>
            </DialogContent>
            <DialogActions>
                <Button
                    onClick={sendButtonHandler}
                >Prześlij</Button>
            </DialogActions>
        </Dialog>
    );
}

export default RateOrder;
