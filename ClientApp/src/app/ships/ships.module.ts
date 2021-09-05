import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { ShipsRoutingModule } from './ships-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { ShipListComponent } from './ship-list/ship-list.component';
import { CreateEditShipComponent } from './create-edit-ship/create-edit-ship.component';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        ShipsRoutingModule
    ],
    declarations: [
        LayoutComponent,
        ShipListComponent,
        CreateEditShipComponent
    ]
})
export class ShipModule { }