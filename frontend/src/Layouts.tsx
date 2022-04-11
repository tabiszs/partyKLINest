import ContentContainer from "./ContentContainer";
import { Outlet } from "react-router-dom";
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
            { label: 'Dodaj', linkTo: '/postAnnouncement', active: true },
            { label: 'Historia', onClick: () => alert('Historia') },
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
            { label: 'Dodaj', linkTo: '/postAnnouncement', active: true },
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
            { label: 'Dodaj', linkTo: '/postAnnouncement', active: true },
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
    return (
        <>
            <Sidebar
                headerHeight={props.headerHeight}
                topButtons={props.topButtons}
                bottomButtons={props.bottomButtons}
            />
            <ContentContainer headerHeight={props.headerHeight}>
                <Outlet />
            </ContentContainer>
        </>
    );
}

