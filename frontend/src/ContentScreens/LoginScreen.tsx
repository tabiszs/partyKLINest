import Button from "@mui/material/Button";
import ContentContainer from "../ContentContainer";

interface LoginScreenProps {
    headerHeight: string;
    login: () => void;
}

const LoginScreen = (props: LoginScreenProps) => {
    return (
        <>
            <ContentContainer headerHeight={props.headerHeight}>
                <Button onClick={props.login}>Zaloguj się</Button>
            </ContentContainer>
        </>
    );
}

export default LoginScreen;