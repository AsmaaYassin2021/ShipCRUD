import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateEditShipComponent } from './create-edit-ship/create-edit-ship.component';
import { LayoutComponent } from './layout/layout.component';
import { ShipListComponent } from './ship-list/ship-list.component';



const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: '', component: ShipListComponent },
            { path: 'add', component: CreateEditShipComponent },
            { path: 'edit/:code', component: CreateEditShipComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ShipsRoutingModule { }