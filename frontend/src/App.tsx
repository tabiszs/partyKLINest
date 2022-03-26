import Header from './Header';
import Sidebar from './Sidebar';
import ContentContainer from './ContentContainer';
import './App.css';
import { Outlet } from 'react-router-dom';

const headerHeight = '6em';

const App = () => {
  return (
    <div className='App'>
      <Header height={headerHeight} />
      <div className='site-container'>
        <Sidebar
          headerHeight={headerHeight}
          topButtons={[
            { label: 'Pulpit', onClick: () => alert('Pulpit') },
            { label: 'Dodaj', onClick: () => alert('Dodaj'), active: true },
            { label: 'Historia', onClick: () => alert('Historia') },
          ]}
          bottomButtons={[
            { label: 'Ustawienia', onClick: () => alert('Ustawienia') },
            { label: 'Wyloguj', onClick: () => alert('Wyloguj') }
          ]}
        />
        <ContentContainer headerHeight={headerHeight}>
          <Outlet/>
        </ContentContainer>
      </div>
    </div>
  );
}

export default App;
