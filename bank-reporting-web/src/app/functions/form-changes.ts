import { FormGroup } from "@angular/forms";

export function isFormChange<T>(obj: T, form: FormGroup): boolean {
    let changed = false;
    
    Object.keys(form.controls).forEach(key => {
        const initial = obj ? obj[key as keyof T] : null;
    
        let newValue = form.controls[key].value;
    
        newValue = newValue === '' ? null : newValue;
    
        if (initial != newValue) {
            changed = true;
        }
    });

    // return
    return changed;
  }