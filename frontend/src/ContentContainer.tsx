import {PropsWithChildren} from 'react';
import './ContentContainer.css';

export interface ContentContainerProps {
  headerHeight: string;
}

const ContentContainer = (props: PropsWithChildren<ContentContainerProps>) => {
  return (
    <div className='content-container' style={{marginTop: props.headerHeight}}>
      {props.children}
    </div>
  );
}

export default ContentContainer;
