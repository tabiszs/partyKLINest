import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import './Sidebar.css';
import { Link } from 'react-router-dom';
import MaterialLink from '@mui/material/Link'; 

interface SidebarButtonProps {
  label: string;
  active?: boolean;
  onClick: () => void;
}

const SidebarButton = (props: SidebarButtonProps) => {
  return (
    <Button
      className='sidebar-button'
      variant='contained'
      onClick={props.onClick}
      color={props.active ? 'secondary' : 'primary'}
    >
      {props.label}
    </Button>
  );
}

interface SidebarLinkButtonProps {
  href: string;
  label: string;
  active?: boolean;
}

const SidebarLinkButton = (props: SidebarLinkButtonProps) => {
  return (
    <Button
      className='sidebar-button'
      variant='contained'
      color={props.active ? 'secondary' : 'primary'}
      component={Link}
      to={props.href}
    >
      {props.label}
    </Button>
  );
}

export interface SidebarContentProps {
  headerHeight: string;
  logoutHandler: () => void;
}

const SidebarContent = (props: SidebarContentProps) => {

  return (
    <div className='sidebar-content-container'>
      <div className='sidebar-buttons-container' style={{marginTop: props.headerHeight}}>
        <div className='sidebar-top-buttons-container'>
          <SidebarLinkButton
            label='Pulpit'
            active
            href='/'
          />
          <SidebarButton
            label='Dodaj'
            onClick={() => alert('test')}
          />
          <SidebarButton
            label='Historia'
            onClick={() => alert('test')}
          />
        </div>
        <div className='sidebar-bottom-buttons-container'>
          <SidebarLinkButton
            label='Ustawienia'
            href="/settings"
          />
          <SidebarButton
            label='Wyloguj'
            onClick={props.logoutHandler}
          />
        </div>
      </div>
    </div>
  );
}

export interface SidebarProps {
  headerHeight: string;
  logoutHandler: () => void;
}

const Sidebar = (props: SidebarProps) => {
  return (
    <div className='sidebar-container'>
      <Box
        sx={{
          backgroundColor: 'primary.dark',
          width: '18em',
          height: '100%'
        }}
      >
        <SidebarContent headerHeight={props.headerHeight} logoutHandler={props.logoutHandler}/>
      </Box>
    </div>
  );
}

export default Sidebar;
