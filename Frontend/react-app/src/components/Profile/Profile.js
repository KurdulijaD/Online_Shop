import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { ProfileModel } from "../../models/UserModels";
import { getMyProfile, update } from "../../services/UserService";
import { Alert, AlertTitle, Button, Box, TextField } from "@mui/material";
import styles from "./Profile.module.css";
import NavBar from "../NavBar/NavBar";

const isEmpty = (value) => value.trim() === "";
const isEmail = (value) => value.includes("@");
const exceptionRead = (value) => value.split(":")[1].split("at")[0];

const Profile = () => {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    Username: "",
    Email: "",
    Password: "",
    OldPassword: "",
    FirstName: "",
    LastName: "",
    BirthDate: "",
    Address: "",
    Image: "",
  });

  const [selectedImage, setSelectedImage] = useState(null);

  const [alert, setAlert] = useState({
    message: "",
    severity: "success",
  });

  const [isValid, setIsValid] = useState({
    email: true,
    username: true,
    password: true,
    oldPassword: true,
    firstName: true,
    lastName: true,
    birthdate: true,
    address: true,
  });

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await getMyProfile();
        console.log("ovaj ispis", response.data);
        setFormData({
          ...formData,
          FirstName: response.data.firstName,
          LastName: response.data.lastName,
          Username: response.data.username,
          Email: response.data.email,
          BirthDate: response.data.birthDate.split("T")[0],
          Address: response.data.address,
        });
        setSelectedImage(response.data.image);
        console.log(selectedImage);
      } catch (error) {
        setAlert({
          message: "",
          severity: "error",
        });
        return;
      }
    };

    fetchData();
  }, []);

  const submitHandler = async (event) => {
    event.preventDefault();

    if (isEmpty(formData.FirstName))
      setIsValid({
        ...isValid,
        firstName: false,
      });
    else
      setIsValid({
        ...isValid,
        firstName: true,
      });
    if (isEmpty(formData.LastName))
      setIsValid({
        ...isValid,
        lastName: false,
      });
    else
      setIsValid({
        ...isValid,
        lastName: true,
      });
    if (isEmpty(formData.Address)) {
      setIsValid({
        ...isValid,
        address: false,
      });
      console.log(isValid.address);
    } else
      setIsValid({
        ...isValid,
        address: true,
      });
    if (isEmpty(formData.Username))
      setIsValid({
        ...isValid,
        username: false,
      });
    else
      setIsValid({
        ...isValid,
        username: true,
      });
    if (isEmpty(formData.Email) && !isEmail(formData.Email))
      setIsValid({
        ...isValid,
        email: false,
      });
    else
      setIsValid({
        ...isValid,
        email: true,
      });
    if (isEmpty(formData.BirthDate))
      setIsValid({
        ...isValid,
        birthdate: false,
      });
    else
      setIsValid({
        ...isValid,
        birthdate: true,
      });

    if (
      (!isEmpty(formData.OldPassword) && isEmpty(formData.Password)) ||
      (isEmpty(formData.OldPassword) && !isEmpty(formData.Password))
    ) {
      console.log(formData.OldPassword);
      console.log(formData.Password);
      setAlert({
        message:
          "For changing password you must fill old and new password field!",
        severity: "error",
      });
      return;
    }

    const formIsValid = Object.values(isValid).every((value) => value === true);
    if (!formIsValid) {
      setAlert({
        message: "You must fill in all fields",
        severity: "error",
      });
      return;
    }

    try {
      const response = await update(formData);
      console.log(response);
      console.log("usao");
      setAlert({
        message: "You updated yours profile settings!",
        severity: "success",
      });
    } catch (error) {
      setAlert({
        message: exceptionRead(error.response.data),
        severity: "error",
      });
      return;
    }
    console.log(isValid);
  };

  return (
    <>
      <NavBar />
      <Box
        sx={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "100vh",
          backgroundColor: "#243b55",
        }}
      >
        {alert.message !== "" && (
          <Alert
            className={styles.alert_register}
            onClose={() => setAlert({ message: "", severity: "success" })}
          >
            <AlertTitle>
              {alert.severity.charAt(0).toUpperCase() + alert.severity.slice(1)}
            </AlertTitle>
            {alert.message}
          </Alert>
        )}
        <form onSubmit={submitHandler}>
          <h2 className={styles["h2"]}>PROFILE</h2>
          <Box sx={{ m: 2 }}>
            <TextField
              sx={{ background: "white" }}
              label="First Name"
              name="FirstName"
              value={formData.FirstName}
              error={!isValid.firstName}
              helperText={!isValid.firstName && "Please enter your first name"}
              onChange={(e) =>
                setFormData({ ...formData, FirstName: e.target.value })
              }
            />
          </Box>
          <Box sx={{ m: 2 }}>
            <TextField
              sx={{ background: "white" }}
              label="Last Name"
              name="LastName"
              value={formData.LastName}
              error={!isValid.lastName}
              helperText={!isValid.lastName && "Please enter your last name"}
              onChange={(e) =>
                setFormData({ ...formData, LastName: e.target.value })
              }
            />
          </Box>
          <Box sx={{ m: 2 }}>
            <TextField
              sx={{ background: "white" }}
              label="Username"
              name="Username"
              value={formData.Username}
              error={!isValid.username}
              helperText={!isValid.username && "Please enter a username"}
              onChange={(e) =>
                setFormData({ ...formData, Username: e.target.value })
              }
            />
          </Box>
          <Box sx={{ m: 2 }}>
            <TextField
              sx={{ background: "white" }}
              label="Email"
              name="Email"
              value={formData.Email}
              error={!isValid.email}
              helperText={!isValid.email && "Please enter a valid email"}
              onChange={(e) =>
                setFormData({ ...formData, Email: e.target.value })
              }
            />
          </Box>
          <Box sx={{ m: 2 }}>
            <TextField
              sx={{ background: "white" }}
              label="Old Password"
              name="oldPassword"
              type="password"
              error={!isValid.oldPassword}
              helperText={
                !isValid.oldPassword && "Please enter your old password"
              }
              onChange={(e) =>
                setFormData({ ...formData, OldPassword: e.target.value })
              }
            />
          </Box>
          <Box sx={{ m: 2 }}>
            <TextField
              sx={{ background: "white" }}
              label="New Password"
              name="password"
              type="password"
              error={!isValid.password}
              helperText={!isValid.password && "Please enter a new password"}
              onChange={(e) =>
                setFormData({ ...formData, Password: e.target.value })
              }
            />
          </Box>
          <Box sx={{ m: 2 }}>
            <TextField
              sx={{ background: "white" }}
              label="Address"
              name="Address"
              value={formData.Address}
              error={!isValid.address}
              helperText={!isValid.address && "Please enter your address"}
              onChange={(e) =>
                setFormData({ ...formData, Address: e.target.value })
              }
            />
          </Box>
          <Box sx={{ m: 2 }}>
            <TextField
              sx={{ background: "white" }}
              label="Birthdate"
              name="birthdate"
              type="date"
              value={formData.BirthDate}
              error={!isValid.birthdate}
              helperText={!isValid.birthdate && "Please enter your birthdate"}
              onChange={(e) =>
                setFormData({ ...formData, BirthDate: e.target.value })
              }
            />
          </Box>
          <Box sx={{ m: 2 }}>
            <input
              accept="image/*"
              id="upload-image"
              type="file"
              onChange={(e) =>
                setFormData({ ...formData, Image: e.target.value })
              }
              style={{ display: "none" }}
            />
            <label htmlFor="upload-image">
              <Button component="span" variant="outlined">
                Upload New Profile Picture
              </Button>
            </label>
          </Box>
          {selectedImage && (
            <Box sx={{ m: 2 }}>
              <img
                // src={URL.createObjectURL(selectedImage)}
                alt="Selected"
                style={{ width: "100px", height: "100px" }}
              />
            </Box>
          )}
          <Button
            type="submit"
            sx={{
              marginLeft: "25%",
              color: "#141e30",
              fontSize: "20px",
              padding: "10px 20px",
              "&:hover": {
                backgroundColor: "#03e9f4",
              },
            }}
            variant="contained"
          >
            Save
          </Button>
        </form>
      </Box>
    </>
  );
};

export default Profile;
