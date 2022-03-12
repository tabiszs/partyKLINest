import Header from './Header';
import Sidebar from './Sidebar';
import ContentContainer from './ContentContainer';
import PostAnnouncement from './ContentScreens/PostAnnouncement';
import './App.css';

const headerHeight = '6em';

const App = () => {
  return (
    <div className='App'>
      <Header height={headerHeight} />
      <div className='site-container'>
        <Sidebar headerHeight={headerHeight} />
        <ContentContainer headerHeight={headerHeight}>
          <PostAnnouncement />
        </ContentContainer>
      </div>
    </div>
  );
}

export default App;
