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
        <Sidebar headerHeight={headerHeight} />
        <ContentContainer headerHeight={headerHeight}>
          <Outlet/>
        </ContentContainer>
      </div>
    </div>
  );
}

export default App;
