import { Dialog, DialogTitle, DialogContent, Typography, Rating, DialogContentText, DialogActions, Button } from "@mui/material";
import RatingStructure from "../DataClasses/Rating";
import UserType from "../DataClasses/UserType";

interface RatingPopupProps {
    opinion: RatingStructure;
    open: boolean;
    userType: UserType;
    cancelDialog: () => void;
}

const RatingPopup = (props: RatingPopupProps) => {

    const title = props.userType === UserType.Cleaner ? "Opinia sprzątającego" : "Opinia klienta";

    return (
        <Dialog open={props.open} onClose={props.cancelDialog} fullWidth={true} maxWidth='sm'>
            <DialogTitle>{title}</DialogTitle>
            <DialogContent>
                <Typography component="legend">Ocena</Typography>
                <Rating readOnly value={props.opinion.rating / 2} precision={0.5} />
                <Typography component="legend">Komentarz</Typography>
                <DialogContentText>{props.opinion.comment}</DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={props.cancelDialog}>OK</Button>
            </DialogActions>
        </Dialog>
    );
}

export default RatingPopup;