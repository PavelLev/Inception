import { HomeComponent } from "./Home/HomeComponent";
import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { TestResultHistoryComponent } from "./Home/TestResultHistoryComponent";

const Routes: Routes = [
    {
        component: TestResultHistoryComponent,
        path: ":id"
    },
    {
        component: HomeComponent,
        path: "",
        pathMatch: "full" 
    }
];

@NgModule
    (
    {
        exports: 
        [
            RouterModule
        ],
        imports: 
        [
            RouterModule.forRoot(Routes)
        ]
    }
    )

export class AppRouting 
{

}
