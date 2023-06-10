import React, {useRef, useState, useContext} from "react";
import { Link, useNavigate } from "react-router-dom";
import AuthContext from "../../contexts/auth-context";
import styles from "./Login.module.css";
import {Alert, AlertTitle} from '@mui/material';
import { GoogleLogin } from '@react-oauth/google';
import { toast } from 'react-toastify';

const isEmpty = (value) => value.trim().length === 0;

const Login = () => {
    const navigate = useNavigate();

    const [isEmailValid, setIsEmailValid] = useState(false);
    const [isPasswordValid, setIsPasswordValid] = useState(false);
    const [alert, setAlert] = useState({
        message: '',
        severity: 'success'
      })

    const authCtx = useContext(AuthContext);
    const emailInputRef = useRef();
    const passwordInputRef = useRef();

    const submitHandler = (event) => {
        event.preventDefault();
        const enteredEmail = emailInputRef.current.value;
        const enteredPassword = passwordInputRef.current.value;

        setIsEmailValid(false);
        setIsPasswordValid(false);
        
        if(!isEmpty(enteredEmail) && !isEmpty(enteredPassword))
        {
            setIsEmailValid(true);
            setIsPasswordValid(true);
        }
        
        if(isEmailValid && isPasswordValid) {
            const loginData = {email : enteredEmail, password: enteredPassword}
            authCtx.onLogin(loginData).then((response) => {
                console.log(response);
            })
            console.log(loginData);
        }
    }

    const googleLoginHandler = (response) => {
        let data = new FormData();
        data.append("googleToken", response.credential);
        authCtx.googleLogin(data);
      };
    
      const googleLoginErrorHandler = () => {
        toast.error("Google login error", {
          position: "top-center",
          autoClose: 2500,
          closeOnClick: true,
          pauseOnHover: false,
        });
      };

    return (
        <div className={styles['body']}>
                    {alert.message !== '' && (
        <Alert
          className={styles.alert_register}
          onClose={() => setAlert({ message: '', severity: 'success' })}
        >
          <AlertTitle>
            {alert.severity.charAt(0).toUpperCase() + alert.severity.slice(1)}
          </AlertTitle>
          {alert.message}
        </Alert>
      )}
            <div className={styles["login-box"]}>
                <h2>Login</h2>
                <form onSubmit={submitHandler}>
                    <div className={styles["user-box"]}>
                        <input  ref={emailInputRef} id="email" type="text" name="email" required />
                        <label>Email</label>
                    </div>
                    <div className={styles["user-box"]}>
                        <input  ref={passwordInputRef} id="password" type="password" name="password" required />
                        <label>Password</label>
                    </div>
                    <button className={styles['button']} type="submit">Submit</button>
                </form>
                <div>
                    <p className={styles['p']}>Not a member? <Link to='/register'>Register</Link></p>
                    <GoogleLogin
                onSuccess={googleLoginHandler}
                onError={googleLoginErrorHandler}
              />
                </div>
            </div>
        </div>
    );
}

export default Login;