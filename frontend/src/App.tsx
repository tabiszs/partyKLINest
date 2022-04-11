import './App.css';
import { useEffect, useState } from 'react';
import { B2CDeleteAccount, B2CEditProfile, B2CLogin, B2CLogout, RetrieveToken } from './Authentication/MsalService';
import Layout from './Layout';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Dashboard from './ContentScreens/Dashboard';
import Settings from './ContentScreens/Settings';
import PostAnnouncement from './ContentScreens/PostAnnouncement';
import Header from './Header';
import Token from './DataClasses/Token';
import UserType from './DataClasses/UserType';
import ClientDashboard from './ContentScreens/Client/ClientDashboard';
import ClientSettings from './ContentScreens/Client/ClientSettings';
import LoginScreen from './ContentScreens/LoginScreen';
import { AdminLayout, CleanerLayout, ClientLayout } from './Layouts';

const headerHeight = '6em';

const App = () => {

  //const [isLogged, setIsLogged] = useState(false);
  const [token, setToken] = useState<Token | null>(null);

  useEffect(() => {
    RetrieveToken().then((tok) => {
      setToken(tok);
    })
  }, []);

  const login = () => {
    B2CLogin().then((tok) => {
      setToken(tok);
    });
  }

  const logout = () => {
    B2CLogout().then(() => {
      setToken(null);
    }).catch((error) => {
      console.log(error);
    });
  }

  const editProfile = () => {
    B2CEditProfile().then((tok) => {
      if (token !== null) setToken(tok);
    });
  }

  const deleteAccount = () => {
    B2CDeleteAccount().then(() => {
      setToken(null);
    }).catch((error) => console.log(error));
  }


  // return (
  //   <BrowserRouter>
  //     <Routes>
  //       <Route path="/" element={<Layout isLogged={isLogged} login={login} logout={logout}/>}>
  //         <Route index element={<Dashboard/>}/>
  //         <Route path="/settings" element={<Settings logout={() => setIsLogged(false)}/>}/>
  //         <Route path="/postAnnouncement" element={<PostAnnouncement/>}/>
  //       </Route>
  //     </Routes>
  //   </BrowserRouter>
  // );

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Header height={headerHeight} />}>
          {token === null ? <Route path="*" element={<LoginScreen headerHeight={headerHeight} login={login} />} /> : ''}
          {token?.userType === UserType.Client ?
            <Route path="/" element={<ClientLayout headerHeight={headerHeight} logout={logout} />}>
              <Route index element={<ClientDashboard token={token!} />} />
              <Route path="/settings" element={<ClientSettings token={token!} editProfile={editProfile} deleteProfile={deleteAccount} />} />
              <Route path="/postAnnouncement" element={<PostAnnouncement />} />
            </Route> : ''
          }
          {token?.userType === UserType.Cleaner ?
            <Route path="/" element={<CleanerLayout headerHeight={headerHeight} logout={logout} />}>

            </Route> : ''
          }
          {token?.userType === UserType.Administrator ?
            <Route path="/" element={<AdminLayout headerHeight={headerHeight} logout={logout} />}>

            </Route> : ''
          }
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
