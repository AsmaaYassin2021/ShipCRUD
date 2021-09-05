import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { Ship } from 'src/app/_models/ship';
import { ShipService } from 'src/app/_services/ship.service';
import Swal from 'sweetalert2'
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'ship-list',
  templateUrl: './ship-list.component.html',
  styleUrls: ['./ship-list.component.css']

})
export class ShipListComponent implements OnInit {
  p: number = 1;
  displayList: boolean = true;
  ships: Array<Ship> = [];
  constructor(private shipService: ShipService) { }


  ngOnInit(): void {
    this.initializeShipsList();
  }

  trackByMethod(index: number, el: any): number {
    return el.code;
  }
  onCreateSucceeded($event: any) {
    this.displayList = true;
    this.initializeShipsList();
  }
  initializeShipsList() {
    this.shipService.getAllShips()
      .pipe(first())
      .subscribe(res => {
        console.log(res)
        if (res.succeeded) {
          this.ships = res.data;
        }
      });
  }
  navigateToCreateShip() {
    this.displayList = false;
  }
  onCreateCancelled($event: any) {
    if ($event == true) {
      this.displayList = true;

    }
  }

  deleteShip(code: any) {
    const ship = this.ships.find(x => x.code === code);
    if (!ship) return;
    this.shipService.delete(code)
      .pipe(first())
      .subscribe(res => {
        console.log(res)
        if (res.succeeded) {
          this.showMessage(res.data, "Successfully deleted", true);
          this.ships = this.ships.filter(x => x.code !== code)
        }
      }, (httpErrorResponse: HttpErrorResponse) => {

        this.handleHttpErrorResponse(httpErrorResponse);
      },
      )
      
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
    });
  }
    
}