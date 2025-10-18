import { FormControl, FormGroup } from "@angular/forms";

export default class ValidateForm {
  static validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsDirty({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }

  static logValidationErrors(formGroup: FormGroup,formErrors:any,validationMessages:any,btnClick=false): void {
    
  // loop through each key in the FormGroup
  Object.keys(formGroup.controls).forEach((key: string) => {
    // Get a reference to the control using the FormGroup.get() method

    const abstractControl = formGroup.get(key);
  
    // If the control is an instance of FormGroup i.e a nested FormGroup
    // then recursively call this same method (logKeyValuePairs) passing it

    // the FormGroup so we can get to the form controls in it
    if (abstractControl instanceof FormGroup) {

      this.logValidationErrors(abstractControl,formErrors,validationMessages,btnClick);
      // If the control is not a FormGroup then we know it's a FormControl
    } else {
      formErrors[key]='';
      if(abstractControl && abstractControl.invalid
        &&(abstractControl.touched || abstractControl.dirty || btnClick)
        ){
        const messages=validationMessages[key];
        for(const errorKey in abstractControl.errors){
          if(errorKey){

            formErrors[key]+= messages[errorKey]+' ';
            console.log('key',key,'errorKey',errorKey,'formErrors[key]',formErrors[key])
          }
          }
        }
      }
    });
  }

}