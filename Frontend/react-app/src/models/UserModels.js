export class RegisterModel  {
           constructor(Username, Email, Password, RepeatPassword, FirstName, LastName, Birthdate, Address, Image, Type) {
                this.Username = Username;
                this.Email = Email;    
                this.Password = Password;
                this.RepeatPassword = RepeatPassword;
                this.FirstName = FirstName;
                this.LastName = LastName;          
                this.Birthdate = Birthdate;
                this.Address = Address;
                this.Image = Image;
                this.Type = Type;
           }
}

export class ProfileModel  {
     constructor(Username, Email, Password, FirstName, LastName, Birthdate, Address, Image) {
          this.Username = Username;
          this.Email = Email;    
          this.Password = Password;
          this.FirstName = FirstName;
          this.LastName = LastName;          
          this.Birthdate = Birthdate;
          this.Address = Address;
          this.Image = Image;
     }
  }
