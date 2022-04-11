import ContentContainer from "./ContentContainer";
import { Outlet, useLocation } from "react-router-dom";
import Sidebar, { SidebarButtonProps } from "./Sidebar";
import './App.css';

interface LayoutProps {
    headerHeight: string;
    logout: () => void;
}

export const ClientLayout = (props: LayoutProps) => (
    <BaseLayout
        {...props}
        topButtons={[
            { label: 'Pulpit', linkTo: '/' },
            { label: 'Dodaj', linkTo: '/postAnnouncement' },
        ]}
        bottomButtons={[
            { label: 'Ustawienia', linkTo: '/settings' },
            { label: 'Wyloguj', onClick: props.logout }
        ]}
    />
);

export const CleanerLayout = (props: LayoutProps) => (
    <BaseLayout
        {...props}
        topButtons={[
            { label: 'Pulpit', linkTo: '/' },
            { label: 'Dodaj', linkTo: '/postAnnouncement' },
            { label: 'Historia', onClick: () => alert('Historia') },
        ]}
        bottomButtons={[
            { label: 'Ustawienia', linkTo: '/settings' },
            { label: 'Wyloguj', onClick: props.logout }
        ]}
    />
);

export const AdminLayout = (props: LayoutProps) => (
    <BaseLayout
        {...props}
        topButtons={[
            { label: 'Pulpit', linkTo: '/' },
            { label: 'Dodaj', linkTo: '/postAnnouncement' },
            { label: 'Historia', onClick: () => alert('Historia') },
        ]}
        bottomButtons={[
            { label: 'Ustawienia', linkTo: '/settings' },
            { label: 'Wyloguj', onClick: props.logout }
        ]}
    />
);

interface BaseLayoutProps extends LayoutProps {
    topButtons: SidebarButtonProps[];
    bottomButtons: SidebarButtonProps[];
}

export const BaseLayout = (props: BaseLayoutProps) => {

    const location = useLocation();

    const mapButtons = (buttons: SidebarButtonProps[]) => buttons.map((button) => ({...button, active: location.pathname === button.linkTo }));

    return (
        <>
            <Sidebar
                headerHeight={props.headerHeight}
                topButtons={mapButtons(props.topButtons)}
                bottomButtons={mapButtons(props.bottomButtons)}
            />
            <ContentContainer headerHeight={props.headerHeight}>
                <Outlet />
            </ContentContainer>
        </>
    );
}

