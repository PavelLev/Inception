import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { AppComponent } from "./AppComponent";
import { HomeComponent } from "./Home/HomeComponent";

import { AppRouting } from "./AppRouting";
import { AlertModule, ButtonsModule } from "ngx-bootstrap";
import { TestResultHistoryListComponent } from "./Home/TestResultHistoryListComponent";
import { Resolver } from "./Resolver";
import { TestingService } from "./Services";
import { TestResultHistoryComponent } from "./Home/TestResultHistoryComponent";
import { LinkTestResultComponent } from "./Home/LinkTestResultComponent";
import { SiteTestOverviewComponent } from "./Home/SiteTestOverviewComponent";
import { MaterialModule } from "./MaterialModule";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { DomainNameService } from "./Home/DomainNameService";
import { OverlaySettingsService } from "./OverlaySettingsService";
import { SiteTestResultService } from "./Home/SiteTestResultService";
import { RemoveScheme } from "./Home/RemoveSchemeService";
@NgModule
    (
    {
        bootstrap:
        [
            AppComponent
        ],
        declarations:
        [
            AppComponent,
            HomeComponent,
            TestResultHistoryListComponent,
            TestResultHistoryComponent,
            LinkTestResultComponent,
            SiteTestOverviewComponent,
            RemoveScheme
        ],
        imports:
        [
            AlertModule.forRoot(),
            ButtonsModule.forRoot(),
            BrowserModule.withServerTransition
                (
                {
                    appId: "ng-cli-universal"
                }
                ),
            HttpClientModule,
            FormsModule,
            AppRouting,
            MaterialModule,
            ReactiveFormsModule,
            BrowserAnimationsModule
        ],
        providers:
        [
            Resolver,
            TestingService,
            DomainNameService,
            OverlaySettingsService,
            SiteTestResultService
        ]
    }
    )

export class AppModule
{

}
