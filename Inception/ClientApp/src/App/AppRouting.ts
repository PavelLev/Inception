import { HomeComponent } from "./Home/HomeComponent";
import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";

const Routes: Routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(Routes)],
  exports: [RouterModule],
})
export class AppRouting { }

