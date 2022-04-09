import './App.css';
import {useEffect, useState} from 'react';
import {B2CLogin, B2CLogout, RetrieveToken} from './Authentication/MsalService';
import Layout from './Layout';
import {BrowserRouter, Routes, Route} from 'react-router-dom';
import Dashboard from './ContentScreens/Dashboard';
import Settings from './ContentScreens/Settings';
import PostAnnouncement from './ContentScreens/PostAnnouncement';
import ComissionForm from './ContentScreens/Admin/ComissionForm';

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
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout isLogged={isLogged} login={login} logout={logout} />}>
          <Route index element={<Dashboard />} />
          <Route path="/settings" element={<Settings logout={() => setIsLogged(false)} />} />
          <Route path="/postAnnouncement" element={<PostAnnouncement />} />
          <Route path="/comission" element={<ComissionForm />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
