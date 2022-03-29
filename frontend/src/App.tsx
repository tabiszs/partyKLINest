import Header from './Header';
import Sidebar from './Sidebar';
import ContentContainer from './ContentContainer';
import './App.css';
import { Outlet } from 'react-router-dom';
import { useEffect, useState } from 'react';
import Button from '@mui/material/Button';
import { B2CLogin, B2CLogout, RetrieveToken } from './Authentication/MsalService';

const headerHeight = '6em';

const App = () => {

  const [isLogged, setIsLogged] = useState(false);

  useEffect(() => {
    RetrieveToken().then((tok) => {
      setIsLogged(tok !== undefined);
    })
  }, []);

  const login = () => {
    B2CLogin().then((tok) => {
      setIsLogged(tok !== undefined);
    });
  }

  const logout = () => {
    B2CLogout().then(() => {
      setIsLogged(false);
    }).catch((error) => {
      console.log(error);
    });
  }

  return (
    <div className='App'>
      <Header height={headerHeight} isLogged={isLogged} />  
      <div className='site-container'>
        { isLogged ? 
          <AfterLoginContent logout={logout}/>
          :
          <BeforeLoginContent login={login}/>     
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

export default App;
