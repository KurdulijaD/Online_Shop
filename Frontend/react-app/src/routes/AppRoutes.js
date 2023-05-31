import React, { useContext} from "react";
import { Routes, Route, Navigate, BrowserRouter } from "react-router-dom";
import AuthContext from "../contexts/auth-context.js";
import Login  from "../components/Login/Login.js"
import Register from "../components/Register/Register.js";

const AppRoutes = () => {
    const authCtx = useContext(AuthContext);

    const isLoggedIn = authCtx.isLoggedIn;
    const role = authCtx.role;
    const verification = authCtx.verification;

    const isAdmin = role === 'ADMINISTRATOR';
    const isCustomer = role === 'CUSTOMER';
    const isSalesman = role === 'SALESMAN';
    const isVerified = verification === 'ACCEPTED';

    return (
        <Routes>
          <Route
            path="/"
            element={isLoggedIn ? <Navigate to="/home" /> : <Login />}
          />
          <Route
            path="/register"
            element={isLoggedIn ? <Navigate to="/home" /> : <Register />}
          />
        </Routes>
    );
}

export default AppRoutes;