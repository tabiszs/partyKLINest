import AppBar from '@mui/material/AppBar';
import './Header.css';

export interface HeaderProps {
  height: string;
}

const Header = (props: HeaderProps) => {
  return (
    <AppBar>
      <div className='header-content' style={{height: props.height}}>
        <h1 className='website-name'>PartyKLINer</h1>
      </div>
    </AppBar>
  );
}

export default Header;
