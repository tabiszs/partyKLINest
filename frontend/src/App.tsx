import './App.css';
import {useEffect, useState} from 'react';
import {B2CDeleteAccount, B2CEditProfile, B2CLogin, B2CLogout, RetrieveToken} from './Authentication/MsalService';
import {BrowserRouter, Routes, Route} from 'react-router-dom';
import PostAnnouncement from './ContentScreens/PostAnnouncement';
import Header from './Header';
import Token from './DataClasses/Token';
import UserType from './DataClasses/UserType';
import ClientDashboard from './ContentScreens/Client/ClientDashboard';
import ClientSettings from './ContentScreens/Client/ClientSettings';
import LoginScreen from './ContentScreens/LoginScreen';
import {AdminLayout, CleanerLayout, ClientLayout} from './Layouts';
import UserBanning from './ContentScreens/Admin/UserBanning';
import ComissionForm from './ContentScreens/Admin/ComissionForm';
import OrderDeletion from './ContentScreens/Admin/OrderDeletion';
import Schedule from './ContentScreens/Cleaner/Schedule';
import AdminDashboard from './ContentScreens/Admin/AdminDashboard';
import CleanerDashboard from './ContentScreens/Cleaner/CleanerDashboard';
import BanScreen from './ContentScreens/BanScreen';

const headerHeight = '6em';

const App = () => {

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
      if (tok !== null) setToken(tok);
    });
  }

  const deleteAccount = () => {
    B2CDeleteAccount().then(() => {
      setToken(null);
    }).catch((error) => console.log(error));
  }

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Header height={headerHeight} />}>
          {token === null ?
            <>
              <Route index element={<LoginScreen headerHeight={headerHeight} login={login} />} />
              <Route path="*" element={<LoginScreen headerHeight={headerHeight} login={login} />} />
            </> : ''}
          {token?.isBanned ?
            <Route path="/" element={<BanScreen headerHeight={headerHeight}/>} />
            : ''}
          {!token?.isBanned && token?.userType === UserType.Client ?
            <Route path="/" element={<ClientLayout headerHeight={headerHeight} logout={logout} />}>
              <Route index element={<ClientDashboard token={token!} />} />
              <Route path="/settings" element={<ClientSettings token={token!} editProfile={editProfile} deleteProfile={deleteAccount} />} />
              <Route path="/postAnnouncement" element={<PostAnnouncement />} />
            </Route> : ''
          }
          {!token?.isBanned && token?.userType === UserType.Cleaner ?
            <Route path="/" element={<CleanerLayout headerHeight={headerHeight} logout={logout} />}>
              <Route index element={<CleanerDashboard token={token!} />} />
              <Route path="/schedule" element={<Schedule token={token!} />} />
              <Route path="/settings" element={<ClientSettings token={token!} editProfile={editProfile} deleteProfile={deleteAccount} />} />
            </Route> : ''
          }
          {!token?.isBanned && token?.userType === UserType.Administrator ?
            <Route path="/" element={<AdminLayout headerHeight={headerHeight} logout={logout} />}>
              <Route index element={<AdminDashboard token={token!} editProfile={editProfile} deleteProfile={deleteAccount} />} />
              <Route path="/orderDeletion" element={<OrderDeletion />} />
              <Route path="/comission" element={<ComissionForm />} />
              <Route path="/banUser" element={<UserBanning />} />
            </Route> : ''
          }
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
