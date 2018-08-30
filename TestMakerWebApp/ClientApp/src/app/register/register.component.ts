import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.less']
})

export class RegisterComponent {

  title: string;
  form: FormGroup;
 
  constructor(private router: Router,
    private userService: UserService,
    private fb: FormBuilder, 
    @Inject('BASE_URL') private baseUrl: string)
  {
    this.title = "New User Registration";
    this.createForm();
  }

  createForm() {
    this.form = this.fb.group({
      UserName: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email] ],
      Password: ['', Validators.required],
      PasswordConfirm: ['', Validators.required],
      DisplayName: ['', Validators.required]
    }, {
        validator: this.passwordConfirmValidator
      });
  }

  onSubmit() {
    
    let tempUser = <User>{};
    tempUser.UserName = this.form.value.UserName;
    tempUser.Email = this.form.value.Email;
    tempUser.Password = this.form.value.Password;
    tempUser.DisplayName = this.form.value.DisplayName;

    this.userService.saveUser(tempUser)
      .subscribe(res => {
        if (res) {
          var v = res;
          console.log("User " + v.UserName + " has been created.");
          this.router.navigate(["login"]);
        }
        else {
          this.form.setErrors({
            "register": "User registration failed."
          });
        }
      }, error => console.log(error));
  }

  onBack() {
    this.router.navigate(["home"]);
  }

  passwordConfirmValidator(control: FormControl): any {
    let p = control.root.get('Password');
    let pc = control.root.get('PasswordConfirm');
    if (p && pc) {
      if (p.value !== pc.value) {
        pc.setErrors(
          { "PasswordMismatch": true }
        );
      }
      else {
        pc.setErrors(null);
      }
    }
    return null;
  }

  // retrieve a FormControl
  getFormControl(name: string) {
    return this.form.get(name);
  }

  // returns TRUE if the FormControl is valid
  isValid(name: string) {
    var e = this.getFormControl(name);
    return e && e.valid;
  }
  // returns TRUE if the FormControl has been changed
  isChanged(name: string) {
    var e = this.getFormControl(name);
    return e && (e.dirty || e.touched);
  }
  // returns TRUE if the FormControl is invalid after user changes
  hasError(name: string) {
    var e = this.getFormControl(name);
    return e && (e.dirty || e.touched) && !e.valid;
  }
}
