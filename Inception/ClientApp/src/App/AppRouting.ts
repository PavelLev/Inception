import { HomeComponent } from "./Home/HomeComponent";
import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { TestResultHistoryComponent } from "./Home/TestResultHistoryComponent";

const Routes: Routes = [
    { path: ":id", component: TestResultHistoryComponent },
    { path: "", component: HomeComponent, pathMatch: "full" }
];

@NgModule({
    imports: [RouterModule.forRoot(Routes)],
    exports: [RouterModule],
})
export class AppRouting { }

