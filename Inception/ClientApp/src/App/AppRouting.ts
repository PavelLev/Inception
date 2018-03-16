import { HomeComponent } from "./Home/HomeComponent";
import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { TestResultHistoryComponent } from "./Home/TestResultHistoryComponent";
import { SiteTestOverviewComponent } from "./Home/SiteTestOverviewComponent";

const Routes: Routes = [
    {
        component: SiteTestOverviewComponent,
        path: "overview/:domain",
        pathMatch: "full"
    },
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
