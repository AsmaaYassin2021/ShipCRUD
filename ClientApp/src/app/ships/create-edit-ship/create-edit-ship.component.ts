import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ShipService } from 'src/app/_services/ship.service';
import { Observable, of } from 'rxjs';
import 'rxjs/add/operator/map';
import Swal from 'sweetalert2'
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { Ship } from 'src/app/_models/ship';
import { ThrowStmt } from '@angular/compiler';
import { HttpErrorResponse } from '@angular/common/http';
import { ValidateCode, ValidateShipLength, ValidateShipWidth } from 'src/app/shared/ValidateCode';

@Component({
  selector: 'create-edit-ship',
  templateUrl: './create-edit-ship.component.html',
})
export class CreateEditShipComponent implements OnInit {

  loading = false;
  isAddMode!: boolean;
  currentShipcode!: string;
  currentShipName!: string;
  shipForm: FormGroup;

  isErrorFound: boolean | undefined;
  errorMessage: string = "";

  countries: Array<any> = [];

  @Output()
  addCancelled = new EventEmitter<boolean>();
  @Output()
  addSucceeded = new EventEmitter<boolean>();


  constructor(private fb: FormBuilder, private route: ActivatedRoute,
    private router: Router, private shipService: ShipService) {

  }


  ngOnInit(): void {

    this.buildForm();
    this.currentShipcode = this.route.snapshot.params['code'];
    this.isAddMode = !this.currentShipcode;
    if (!this.isAddMode) {
      this.shipService.getShipByCode(this.currentShipcode)
        .pipe(first())
        .subscribe(res => {
          console.log(res)
          if (res.succeeded) {
            console.log(res.data);
            this.currentShipName = res.data.name;
            this.shipForm.setValue(res.data);
          }
        }, error => {
          this.showMessage(error.errors, "Failed", true)
        });
      this.shipForm.setControl('code', new FormControl('', {
        updateOn: 'blur'
      }));
    }
  }

  buildForm() {
    this.shipForm = this.fb.group({

      'code': ['', {
        validators: [
          Validators.required,
          Validators.maxLength(100),
          ValidateCode,
        ],
        updateOn: 'blur'
      }],
      'shipWidth': ['', {
        validators: [
          Validators.required,
          ValidateShipWidth,
        ],
        updateOn: 'blur'
      }],
      'shipLength': ['', {
        validators: [
          Validators.required,
          ValidateShipLength,
        ],
        updateOn: 'blur'
      }],
      'name': ['', {
        validators: [
          Validators.required,
          Validators.maxLength(100),
          Validators.minLength(2),
        ],
        updateOn: 'blur'
      }]
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.shipForm.controls; }


  isUniqueCode() {
    let val = this.shipForm.get('code')?.value;
      this.shipService.isCodeUnique(val).subscribe(res => {
        if (res.succeeded) {
          if (!res.data) {
            this.shipForm.get('code')?.setErrors({
              "repeated": true
            })
          }
        }
      });
  }
  isUniqueName() {
    let val = this.shipForm.get('name')?.value;
    if (this.currentShipName !== val && val !== null) {
      this.shipService.isNameUnique(val).subscribe(res => {
        if (res.succeeded) {
          if (!res.data) {
            this.shipForm.get('name')?.setErrors({
              "repeated": true
            })
          }
        }
      });
    }
  }

  onSubmit() {


    this.isUniqueName();
    if (this.isAddMode) {
     // this.isUniqueCode();
    }

    for (let c in this.shipForm.controls) {
      this.shipForm.controls[c].markAsTouched();
    }


    if (this.shipForm.invalid) {
      return;
    }
    this.loading = true;
    if (this.isAddMode) {
      this.createShip();
    } else {
      this.updateShip();
    }
  }
  private createShip() {
    this.shipService.create(this.shipForm.value)
      .pipe(first())
      .subscribe(res => {
        console.log(res)
        if (res.succeeded) {
          this.showMessage(res.data, "Ship Created", true);
        }
      }, (httpErrorResponse: HttpErrorResponse) => {

        this.handleHttpErrorResponse(httpErrorResponse);
      },
      )
      .add(() => this.loading = false);
  }

  private updateShip() {
    this.shipService.update(this.shipForm.value)
      .pipe(first())
      .subscribe(res => {
        console.log(res)
        if (res.succeeded) {
          this.showMessage(res.data, "Ship updated", true);
        }
      }, (httpErrorResponse: HttpErrorResponse) => {

        this.handleHttpErrorResponse(httpErrorResponse);
      },
      )
      .add(() => this.loading = false);
  }

  cancelCreate() {
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-labeled btn-info',
        cancelButton: 'btn btn-labeled btn-danger'
      },
      buttonsStyling: false
    })
    swalWithBootstrapButtons.fire({
      title: 'Are you sure?',
      text: 'Do you want to cancel and return to main page!',
      icon: 'info',
      showCancelButton: true,
      confirmButtonText: 'Yes, Cancel!',
      cancelButtonText: 'No!',
    }).then((isConfirm: any) => {
      if (isConfirm) {
        this.addCancelled.emit(true);
        this.router.navigate(['../../'], { relativeTo: this.route });
      }
    });
  }

  showMessage(message: string, title: string, isNavigate: boolean) {
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-labeled btn-info',
      },
      buttonsStyling: false
    })
    swalWithBootstrapButtons.fire({
      title: title,
      text: message,
      icon: 'info',
      showCancelButton: false,
      confirmButtonText: 'Ok',
    }).then((isConfirm: any) => {
      if (isConfirm) {
        if (isNavigate) {
          this.addSucceeded.emit(true);
          this.router.navigate(['../../'], { relativeTo: this.route });
        }
      }
    });
  }
  handleHttpErrorResponse(httpErrorResponse: HttpErrorResponse) {
    if (httpErrorResponse.status == 400) {
      if (!Array.isArray(httpErrorResponse.error.errors)) {
        this.showMessage(httpErrorResponse.error.errors.toString(), "Failed", false)
      }
      else {
        this.showMessage(httpErrorResponse.error.errors, "Failed", false);
      }
    }
    else if (httpErrorResponse.status == 500) {
      this.showMessage("Application Error Occured", "Failed", true)

    }
  }
}

