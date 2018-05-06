import { BrowserModule } from "@angular/platform-browser";
import { NgModule, ErrorHandler } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule, RouteReuseStrategy } from "@angular/router";
import { AppComponent } from "./AppComponent";
import { HomeComponent } from "./Home/HomeComponent";
import { ChartsModule } from 'ng2-charts';

import { AppRouting } from "./AppRouting";
import { AlertModule, ButtonsModule } from "ngx-bootstrap";
import { TestResultHistoryListComponent } from "./Home/TestResultHistoryListComponent";
import { Resolver } from "./Resolver";
import { TestingService } from "./TestingService";
import { TestResultHistoryComponent } from "./Home/TestResultHistoryComponent";
import { LinkTestResultComponent } from "./Home/LinkTestResultComponent";
import { SiteTestOverviewComponent } from "./Home/SiteTestOverviewComponent";
import { MaterialModule } from "./MaterialModule";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { DomainNameService } from "./Home/DomainNameService";
import { OverlaySettingsService } from "./OverlaySettingsService";
import { SiteTestResultService } from "./Home/SiteTestResultService";
import { RemoveScheme } from "./Home/RemoveSchemeService";
import { InceptionRouteReuseStrategy } from "./Home/InceptionRouteReuseStrategy";
import { SiteTestOverviewService } from "./Home/SiteTestOverviewService";
import { ToastErrorHandler } from "./ToastErrorHandler";
import { ToastHttpInterceptor } from "./ToastHttpInterceptor";
import { ToastrModule } from "ngx-toastr";
import { BarChartComponent } from "./Home/charts/BarChart/BarChartComponent";


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
            RemoveScheme,
            BarChartComponent
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
            BrowserAnimationsModule,
            ToastrModule.forRoot    
                (
                {
                    positionClass: "toast-bottom-right",
                    toastClass: "InceptionToast"
                }
                ),
            ChartsModule
        ],
        providers:
        [
            // Resolver,
            TestingService,
            DomainNameService,
            OverlaySettingsService,
            SiteTestResultService,
            SiteTestOverviewService,
            { provide: RouteReuseStrategy, useClass: InceptionRouteReuseStrategy },
            { provide: ErrorHandler, useClass: ToastErrorHandler },
            { provide: HTTP_INTERCEPTORS, useClass: ToastHttpInterceptor, multi: true }
        ]
    }
    )

export class AppModule
{

}
