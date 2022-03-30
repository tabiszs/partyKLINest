import Header from './Header';
import Sidebar from './Sidebar';
import ContentContainer from './ContentContainer';
import './App.css';
import { Outlet } from 'react-router-dom';
import Button from '@mui/material/Button';

const headerHeight = '6em';

interface LayoutProps {
    login: () => void;
    logout: () => void;
    isLogged: boolean;
}

const Layout = (props: LayoutProps) => {

  return (
    <div className='App'>
      <Header height={headerHeight} isLogged={props.isLogged} />  
      <div className='site-container'>
        { props.isLogged ? 
          <AfterLoginContent logout={props.logout}/>
          :
          <BeforeLoginContent login={props.login}/>     
        }
      </div>
    </div>
  );
}

interface AfterLoginContentProps {
  logout: () => void;
}

const AfterLoginContent = (props: AfterLoginContentProps) => {
  return (
    <>
      <Sidebar headerHeight={headerHeight} logoutHandler={props.logout} />
      <ContentContainer headerHeight={headerHeight}>
        <Outlet/>
      </ContentContainer>
    </>
  );
}

interface BeforeLoginContentProps {
  login: () => void;
}

const BeforeLoginContent = (props: BeforeLoginContentProps) => {
  return (
    <>
      <ContentContainer headerHeight={headerHeight}>
        <Button onClick={props.login}>Zaloguj się</Button>
      </ContentContainer>
    </>
  );
}

export default Layout;
