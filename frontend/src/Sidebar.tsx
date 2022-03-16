import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import './Sidebar.css';

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

export interface SidebarContentProps {
  headerHeight: string;
}

const SidebarContent = (props: SidebarContentProps) => {
  return (
    <div className='sidebar-content-container'>
      <div className='sidebar-buttons-container' style={{marginTop: props.headerHeight}}>
        <div className='sidebar-top-buttons-container'>
          <SidebarButton
            label='Pulpit'
            onClick={() => alert('test')}
          />
          <SidebarButton
            label='Dodaj'
            active
            onClick={() => alert('test')}
          />
          <SidebarButton
            label='Historia'
            onClick={() => alert('test')}
          />
        </div>
        <div className='sidebar-bottom-buttons-container'>
          <SidebarButton
            label='Użytkownicy'
            onClick={() => alert('Ustawienia')}
          />
          <SidebarButton
            label='Wyloguj'
            onClick={() => alert('Wyloguj')}
          />
        </div>
      </div>
    </div>
  );
}

export interface SidebarProps {
  headerHeight: string;
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
        <SidebarContent headerHeight={props.headerHeight} />
      </Box>
    </div>
  );
}

export default Sidebar;