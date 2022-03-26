import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import './Sidebar.css';

export interface SidebarButtonProps {
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

interface SidebarTopBottonButtonsProps {
  buttonProps: SidebarButtonProps[];
}

const createButtonsFromProps = (props: SidebarTopBottonButtonsProps) => {
  let buttons = [];
  for (const buttonProps of props.buttonProps) {
    buttons.push(
      <SidebarButton
        label={buttonProps.label}
        onClick={buttonProps.onClick}
        active={buttonProps.active}
      />
    );
  }

  return buttons;
}

const SidebarTopButtons = (props: SidebarTopBottonButtonsProps) => {

  return (
    <div className='sidebar-top-buttons-container'>
      {createButtonsFromProps(props)}
    </div>
  );
}

const SidebarBottomButtons = (props: SidebarTopBottonButtonsProps) => {
  return (
    <div className='sidebar-bottom-buttons-container'>
      {createButtonsFromProps(props)}
    </div>
  );
}

interface SidebarContentProps {
  headerHeight: string;
  topButtons: SidebarButtonProps[];
  bottomButtons: SidebarButtonProps[];
}

const SidebarContent = (props: SidebarContentProps) => {
  return (
    <div className='sidebar-content-container'>
      <div className='sidebar-buttons-container' style={{marginTop: props.headerHeight}}>
        <SidebarTopButtons buttonProps={props.topButtons} />
        <SidebarBottomButtons buttonProps={props.bottomButtons} />
      </div>
    </div>
  );
}

export interface SidebarProps {
  headerHeight: string;
  topButtons: SidebarButtonProps[];
  bottomButtons: SidebarButtonProps[];
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
          <SidebarContent
            headerHeight={props.headerHeight}
            topButtons={props.topButtons}
            bottomButtons={props.bottomButtons}
          />
      </Box>
    </div>
  );
}

export default Sidebar;
