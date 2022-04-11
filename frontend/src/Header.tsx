import AppBar from '@mui/material/AppBar';
import { Outlet } from 'react-router-dom';
import './Header.css';
import './App.css';

export interface HeaderProps {
  height: string;
}

const Header = (props: HeaderProps) => {


  return (
    <div className='App'>
      <AppBar>
        <div className='header-content' style={{ height: props.height }}>
          <h1 className='website-name'>PartyKLINer</h1>
        </div>
      </AppBar>
      <div className='site-container'>
        <Outlet/>
      </div>
    </div>
  );
}

export default Header;
