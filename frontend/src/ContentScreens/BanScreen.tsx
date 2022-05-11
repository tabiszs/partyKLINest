import './BanScreen.css';

interface BanScreenProps {
  headerHeight: string;
};

const BanScreen = (props: BanScreenProps) => {
  return (
    <div className='ban-screen'>
      <div className='ban-pusher' style={{height: props.headerHeight}}></div>
      <h1 className='ban-notice'>Zostałeś zbanowany</h1>
      <div className='ban-text'>Nie możesz już niczego zrobić</div>
    </div>
  );
};

export default BanScreen;
