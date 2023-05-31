export class RegisterModel  {
           constructor(firstname, lastname, username, email, password, repeatPassword, birthdate, address, image, userType) {
            this.firstname = firstname;
            this.lastname = lastname;
            this.username = username;
            this.email = email;
            this.password = password;
            this.repeatPassword = repeatPassword;
            this.birthDate = birthdate;
            this.address = address;
            this.image = image;
            this.userType = userType;
           }
        }
export default RegisterModel;