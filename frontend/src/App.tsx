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
      { isLogged ?
      <div className='site-container'>
        <Sidebar headerHeight={headerHeight} logoutHandler={logout} />
        <ContentContainer headerHeight={headerHeight}>
          <Outlet/>
        </ContentContainer>
      </div> 
      :
      <div className='site-container'>
      <ContentContainer headerHeight={headerHeight}>
        <Button onClick={login}>Zaloguj się</Button>
      </ContentContainer>
      </div> }
    </div>
  );
}

export default App;
