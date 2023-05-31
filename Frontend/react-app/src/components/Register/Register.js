import { useState, useRef } from "react";
import { Link, useNavigate } from "react-router-dom";
import styles from "./Register.module.css";
import RegisterModel from '../../models/UserModels';
import {register} from '../../services/UserService';
import {Alert, AlertTitle} from '@mui/material';

const isNotEmpty = (value) => value.trim() !== '';
const isEmail = (value) => value.includes('@');

const Register = () => {
    const navigate = useNavigate();
    const [userType, setUserType] = useState('Customer');

    const [alert, setAlert] = useState({
        message: '',
        severity: 'success'
      })

    const [isValid, setIsValid] = useState({
        email: false,
        username: false,
        repeatPassword: false,
        password: false,
        firstName: false,
        lastName: false,
        birthdate: false,
        address: false,
    })

    const [errorMessages, setErrorMessages] = useState({
        emailErrorMessage: 'Please enter an email',
        usernameErrorMessage: 'Please enter an username',
        passwordErrorMessage: 'Please enter a password',
        repeatPasswordErrorMessage: 'Please enter a password',
        firstNameErrorMessage: 'Please enter a first name',
        lastNameErrorMessage: 'Please enter a last name',
        birthDateErrorMessage: 'Please enter a birthday',
        addressErrorMessage: 'Please enter an address'
      })

      const firstNameInputRef = useRef();
      const lastNameInputRef = useRef();
      const usernameInputRef = useRef();
      const emailInputRef = useRef();
      const passwordInputRef = useRef();
      const repeatPasswordInputRef = useRef();
      const birthdateInputRef = useRef();
      const addressInputRef = useRef();
      const imageInputRef = useRef();
      const imageRef = useRef();

    const firstNameBlurHandler = () => {
        const enteredFirstName = firstNameInputRef.current.value;
        if(!isNotEmpty(enteredFirstName)) {
            setIsValid((valid) => ({
                ...valid,
                firstName: true
            }))
        }
        else {
            setIsValid((valid) => ({
                ...valid,
                firstName: false
            })) 
        }
    }

    const lastNameBlurHandler = () => {
        const enteredlastName = lastNameInputRef.current.value;
        if(!isNotEmpty(enteredlastName)) {
            setIsValid((valid) => ({
                ...valid,
                lastName: true
            }))
        }
        else {
            setIsValid((valid) => ({
                ...valid,
                lastName: false
            }))
        }
    }

    const usernameBlurHandler = () => {
        const enteredUsername = usernameInputRef.current.value;
        if(!isNotEmpty(enteredUsername)) {
            setIsValid((valid) => ({
                ...valid,
                username: true
            }))
        }
        else {
            setIsValid((valid) => ({
                ...valid,
                username: false
            }))
        }
    }

    const emailBlurHandler = () => {
        const enteredEmail = emailInputRef.current.value;
        if(!isNotEmpty(enteredEmail) && !isEmail(enteredEmail)) {
            setIsValid((valid) => ({
                ...valid,
                email: true
            }))
        }
        else {
            setIsValid((valid) => ({
                ...valid,
                email: false
            }))
        }
    }

    const passwordBlurHandler = () => {
        const enteredPassword = passwordInputRef.current.value;
        if(!isNotEmpty(enteredPassword)) {
            setIsValid((valid) => ({
                ...valid,
                password: true
            }))
        }
        if(enteredPassword.length >= 8) {
            setIsValid((valid) => ({
                ...valid,
                password: true
            }))
        }
        else {
            setIsValid((valid) => ({
                ...valid,
                password: false
            }))
            setErrorMessages((errors) => ({
                ...errors,
                passwordErrorMessage: "Password must contains minimum 8 characters"
            }))
        }
    }

    const repeatPasswordBlurHandler = () => {
        const enteredRepeatPassword = repeatPasswordInputRef.current.value;
        const enteredPassword = passwordInputRef.current.value;
        if(!isNotEmpty(enteredRepeatPassword)) {
            setIsValid((valid) => ({
                ...valid,
                repeatPassword: true
            }))
        }
        else {
            setIsValid((valid) => ({
                ...valid,
                repeatPassword: false
            }))
        }

        if(enteredRepeatPassword !== enteredPassword) {
            setIsValid((valid) => ({
                ...valid,
                repeatPassword: false
            }))
            setErrorMessages((errors) => ({
                ...errors,
                repeatPasswordErrorMessage: "Passwords don't match!"
            }))
        }
    }

    const birthdateBlurHandler = () => {
        const enteredDate = birthdateInputRef.current.value;
        if(!isNotEmpty(enteredDate)) {
            setIsValid((valid) => ({
                ...valid,
                birthdate: true
            }))
        }
        else {
            setIsValid((valid) => ({
                ...valid,
                birthdate: false
            }))
        }

        const selectedDate = new Date(enteredDate)
        const currentDate = new Date()
        const minAgeDate = new Date(
          currentDate.getFullYear() - 18,
          currentDate.getMonth(),
          currentDate.getDate()
        )

        if (selectedDate < minAgeDate) {
            setIsValid((errors) => ({
              ...errors,
              birthdate: false
            }))
            setErrorMessages((errors) => ({
                ...errors,
                birthDateErrorMessage: "You must have 18 years!"
            }))
        }
    }

    const addressBlurHandler = () => {
        const enteredAddress = addressInputRef.current.value;
        if(!isNotEmpty(enteredAddress)) {
            setIsValid((valid) => ({
                ...valid,
                address: true
            }))
        }
        else {
            setIsValid((valid) => ({
                ...valid,
                address: false
            }))
        }
    }

    const handleUserTypeChange = (event) => {
        setUserType(event.target.value);
    };

    const submitHandler = (event) => {
        event.preventDefault();
        let selectedDate = new Date(birthdateInputRef.current.value);
        let selectedImage = imageInputRef.current.files;
        if (selectedImage && selectedImage.length > 0) {
            const reader = new FileReader();
            reader.onload = (e) => {
                imageRef.current.src = e.target.result;
              };
        
              reader.readAsDataURL(selectedImage[0]);
        }

        const enteredRepeatPassword = repeatPasswordInputRef.current.value;
        const enteredPassword = passwordInputRef.current.value;
        
        if(isValid) {
            const registerData = new RegisterModel(
                firstNameInputRef.current.value.trim(),
                lastNameInputRef.current.value.trim(),
                usernameInputRef.current.value.trim(),
                emailInputRef.current.value.trim(),
                passwordInputRef.current.value.trim(),
                repeatPasswordInputRef.current.value.trim(),
                selectedDate,
                addressInputRef.current.value.trim(),
                selectedImage,
                userType.toUpperCase()
            )

            register(registerData)
            .then((response) => {
                setAlert({
                    message: response.data,
                    severity: 'success'
                })
                if(response.ok) {
                    navigate('/');
                    return;
                }
            })
            .catch((error) => {
                setAlert({
                    message: error.response.data.Exception,
                    severity: 'error'
                  })
            })
        }
    };

  
  const firstNameClasses = isValid.firstName ? 'form-control invalid' : 'form-control';
  const lastNameClasses = isValid.lastName ? 'form-control invalid' : 'form-control';
  const emailClasses = isValid.email ? 'form-control invalid' : 'form-control';
  const passwordClasses = isValid.password ? 'form-control invalid' : 'form-control';
  const repeatPasswordClasses = isValid.repeatPassword ? 'form-control invalid' : 'form-control';
  const usernameClasses = isValid.username ? 'form-control invalid' : 'form-control';
  const birthdateClasses = isValid.birthdate ? 'form-control invalid' : 'form-control';
  const addressClasses = isValid.address ? 'form-control invalid' : 'form-control';

  return (    
        <div className={styles["register-box"]}>
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
            <h2>Register</h2>
            <form className={styles['form-control']} onSubmit={submitHandler}>
                <div className={styles['control-group']}>
                    <div className={firstNameClasses}>
                        <label htmlFor="name">First name</label>
                        <input type="text" id="firstName" ref={firstNameInputRef} onBlur={firstNameBlurHandler}/>
                        {isValid.firstName && <p className={styles["error-text"]}>{errorMessages.firstNameErrorMessage}</p>}
                    </div>
                    <div className={lastNameClasses}>
                        <label htmlFor="name">Last name</label>
                        <input type="text" id="lastname" ref={lastNameInputRef} onBlur={lastNameBlurHandler}/>
                        {isValid.lastName && <p className={styles["error-text"]}>{errorMessages.lastNameErrorMessage}</p>}
                    </div>
                    <div className={usernameClasses}>
                        <label htmlFor="name">Username</label>
                        <input type="text" id="username" ref={usernameInputRef} onBlur={usernameBlurHandler}/>
                        {isValid.username && <p className={styles["error-text"]}>{errorMessages.usernameErrorMessage}</p>}
                    </div>
                    <div className={emailClasses}>
                        <label htmlFor="name">Email</label>
                        <input type="text" id="lastname" ref={emailInputRef} onBlur={emailBlurHandler}/>
                        {isValid.email && <p className={styles["error-text"]}>{errorMessages.emailErrorMessage}</p>}
                    </div>
                    <div className={passwordClasses}>
                        <label htmlFor="name">Password</label>
                        <input type="password" id="Password" ref={passwordInputRef} onBlur={passwordBlurHandler}/>
                        {isValid.password && <p className={styles["error-text"]}>{errorMessages.passwordErrorMessage}</p>}
                    </div>
                    <div className={repeatPasswordClasses}>
                        <label htmlFor="name">Repeat password</label>
                        <input type="password" id="repeatPassword" ref={repeatPasswordInputRef} onBlur={repeatPasswordBlurHandler}/>
                        {isValid.repeatPassword && <p className={styles["error-text"]}>{errorMessages.repeatPasswordErrorMessage}</p>}
                    </div>
                    <div className={birthdateClasses}>
                        <label htmlFor="name">Birthdate</label>
                        <input type="date" id="birthdate" ref={birthdateInputRef} onBlur={birthdateBlurHandler}/>
                        {isValid.birthdate && <p className={styles["error-text"]}>{errorMessages.birthDateErrorMessage}</p>}
                    </div>
                    <div className={addressClasses}>
                        <label htmlFor="name">Address</label>
                        <input type="text" id="address" ref={addressInputRef} onBlur={addressBlurHandler}/>
                        {isValid.address && <p className={styles["error-text"]}>{errorMessages.addressErrorMessage}</p>}
                    </div>
                    <div>
                        <label htmlFor="name">Image</label>
                        <input type="file" id="image" ref={imageInputRef}/>
                        <img ref={imageRef} alt="Preview"/>
                    </div>
                    <div className={styles.radioContainer}>
                        <fieldset>
                            <legend>User type</legend>
                            <label>Customer<input type="radio" text="Customer" name="userType" value="Customer" checked={userType === 'Customer'} onChange={handleUserTypeChange}/></label>
                            <label>Salesman <input type="radio" name="userType" value="Salesman" checked={userType === 'Salesman'} onChange={handleUserTypeChange}/></label>
                        </fieldset>
                    </div>
                </div>
                <button className={styles['button']} type="submit">Submit</button>
                <div>
                    <p className={styles['p']}>Already a member? <Link to='/'>Login</Link></p>
                </div>
            </form>
        </div>
  );
};

export default Register;
