import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../services/AuthService.js"
import jwtDecode from 'jwt-decode';

const AuthContext = React.createContext({
    isLoggedIn: false,
    user: null,
    onLogout: () => {},
    onLogin: (logInData) => {},
});

const decodeToken = (token) => {
    console.log('token',token);
    try {
      const decoded = jwtDecode(token);
      return decoded;
    } catch (error) {
      console.log('Error decoding token:', error);
      return null;
    }
  };

export const AuthContextProvider = (props) => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [token, setToken] = useState('');
    const [verification, setVerification] = useState('');
    const [role, setRole] = useState('');

    const navigate = useNavigate();

    useEffect(() => {
        const loggedIn = sessionStorage.getItem('isLoggedIn');
        const currentToken = sessionStorage.getItem('token');
        const currentRole = sessionStorage.getItem('role');
        const currentVerification = sessionStorage.getItem('verification');

        if(loggedIn === '1') {
            setIsLoggedIn(true);
            setToken(currentToken);
            setRole(currentRole);
            setVerification(currentVerification);
        }
    }, []);

    const logInHandler = async(logInData) => {
        try {
            const response = await login(logInData);

            if (!response.ok) {
                throw new Error("Invalid email or password!!!");
            }
            console.log(response);

            const decodedToken = decodeToken(response.data);
            console.log('decoded token: ', decodeToken);
            let verification = decodedToken.Verification;
            let role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

            setIsLoggedIn(true);
            sessionStorage.setItem('isLoggedIn', '1');
            sessionStorage.setItem('token', response);
            sessionStorage.setItem('verification', verification); 
            sessionStorage.setItem('role', role);    
            navigate("/home");  
        } catch (error){
            alert(error.message);
        }
    };

    const logOutHandler = async() => {
            setIsLoggedIn(false);
            sessionStorage.removeItem('isLoggedIn');
            sessionStorage.removeItem('token');    
            sessionStorage.removeItem('verification');
            sessionStorage.removeItem('role');    
            navigate("/login");       
    };

    return (
        <AuthContext.Provider
        value={{
            isLoggedIn: isLoggedIn,
            token: token,
            verification: verification,
            role: role,
            onLogout: logOutHandler,
            onLogin: logInHandler
        }}>
            {props.children}       
        </AuthContext.Provider>
    );
};

export default AuthContext;