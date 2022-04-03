import { BrowserRouter, Routes, Route } from 'react-router-dom';
import PostAnnouncement from './ContentScreens/PostAnnouncement';
import App from './App';

const Router = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<App/>}>
          <Route index element={<div>Welkom op onze site</div>}/>
          <Route path="/postAnnouncement" element={<PostAnnouncement/>}/>
        </Route>
      </Routes>
    </BrowserRouter>);
}

export default Router;
