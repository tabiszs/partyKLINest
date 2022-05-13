import {Dialog, DialogActions, DialogContent, DialogContentText} from "@mui/material";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import {useState} from "react";
import UserInfo from "../DataClasses/UserInfo";
import {postBanUser} from "../Api/endpoints";

interface BanButtonProps {
  onClick: () => void;
}

const BanButton = (props: BanButtonProps) => {
  return (
    <Button
      variant='outlined'
      color='error'
      onClick={props.onClick}
    >
      Zablokuj
    </Button>
  );
}

interface UserCardProps {
  user: UserInfo;
  banUser: () => void;
}

const UserCard = (props: UserCardProps) => {

  const [open, setOpen] = useState(false);

  const openBanDialog = () => {
    setOpen(true);
  }

  const cancelBanDialog = () => {
    setOpen(false);
  }

  const confirmBanDialog = () => {
    props.banUser();
    setOpen(false);
  }

  const displayButton = !props.user.isBanned;

  return (
    <>
      <div className='order-card'>
        <Card variant='outlined'>
          <div className='card-content'>
            <div className='card-column'>
              <strong>{props.user.isBanned ? "(zablokowany) " : ""}{props.user.name} {props.user.surname}</strong>
              <br />
              Typ: {props.user.accountType}
              <br />
              Email: {props.user.email}
            </div>
            <div className='card-column'>
              {displayButton
                ? <BanButton onClick={openBanDialog} />
                : null}
            </div>
          </div>
        </Card>
      </div>
      <Dialog open={open} onClose={cancelBanDialog} aria-describedby='alert-dialog-description'>
        <DialogContent>
          <DialogContentText id='alert-dialog-description'>
            Czy na pewno chcesz zablokować użytkownika {props.user.name} {props.user.surname}?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={confirmBanDialog} style={{color: 'red'}}>Tak</Button>
          <Button onClick={cancelBanDialog} autoFocus>Nie</Button>
        </DialogActions>
      </Dialog>
    </>

  );
}

interface UserListProps {
  users: UserInfo[];
}

const UserList = (props: UserListProps) => {

  const userCards = props.users.map((user) =>
    <UserCard user={user} banUser={async () => {
      await postBanUser(user.oid);
      document.location.reload();
    }} />
  )

  return (
    <div className='user-list-container'>
      {userCards}
    </div>
  );
}

export default UserList;
