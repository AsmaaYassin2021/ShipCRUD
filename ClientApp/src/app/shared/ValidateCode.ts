import { AbstractControl } from '@angular/forms';

export function ValidateCode(control: AbstractControl) {
    const regex = new RegExp('^[a-zA-Z]{4}-[0-9]{4}-[a-zA-Z]{1}[0-9]{1}$');
    const valid = regex.test(control.value.trim());
    return valid ? null : { invalidCode: true };

}
export function ValidateShipWidth(control: AbstractControl) {
    const val = control.value;
    return val<=70&&val>=15 ? null : { invalidWidth: true };

}

export function ValidateShipLength(control: AbstractControl) {
    const val = control.value;
    return val<=400&&val>=50 ? null : { invalidLength: true };

}

