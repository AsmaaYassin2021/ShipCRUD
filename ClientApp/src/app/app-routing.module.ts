import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home';

const shipsModule = () => import('./ships/ships.module').then(x => x.ShipModule);

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'ships', loadChildren: shipsModule },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


