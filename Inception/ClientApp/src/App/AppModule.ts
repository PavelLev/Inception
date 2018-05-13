import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { ErrorHandler, NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { RouteReuseStrategy } from "@angular/router";
import { AlertModule, ButtonsModule } from "ngx-bootstrap";
import { ToastrModule } from "ngx-toastr";
import { AppComponent } from "./AppComponent";
import { AppRouting } from "./AppRouting";
import { DomainNameService } from "./Home/DomainNameService";
import { HomeComponent } from "./Home/HomeComponent";
import { InceptionRouteReuseStrategy } from "./Home/InceptionRouteReuseStrategy";
import { LinkTestResultComponent } from "./Home/LinkTestResultComponent";
import { RemoveScheme } from "./Home/RemoveSchemeService";
import { SiteTestOverviewComponent } from "./Home/SiteTestOverviewComponent";
import { SiteTestOverviewService } from "./Home/SiteTestOverviewService";
import { SiteTestResultService } from "./Home/SiteTestResultService";
import { TestResultHistoryComponent } from "./Home/TestResultHistoryComponent";
import { TestResultHistoryListComponent } from "./Home/TestResultHistoryListComponent";
import { JsonDateHttpInterceptor } from "./JsonDateHttpInterceptor";
import { MaterialModule } from "./MaterialModule";
import { OverlaySettingsService } from "./OverlaySettingsService";
import { TestingService } from "./TestingService";
import { ToastErrorHandler } from "./ToastErrorHandler";
import { ToastHttpInterceptor } from "./ToastHttpInterceptor";



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
            BrowserAnimationsModule,
            ToastrModule.forRoot    
                (
                {
                    positionClass: "toast-bottom-right",
                    toastClass: "InceptionToast"
                }
                )
        ],
        providers:
        [
            // Resolver,
            TestingService,
            DomainNameService,
            OverlaySettingsService,
            SiteTestResultService,
            SiteTestOverviewService,
            {
                provide: RouteReuseStrategy,
                useClass: InceptionRouteReuseStrategy
            },
            {
                provide: ErrorHandler,
                useClass: ToastErrorHandler
            },
            { 
                provide: HTTP_INTERCEPTORS,
                useClass: ToastHttpInterceptor,
                multi: true
            },
            {
                provide: HTTP_INTERCEPTORS,
                useClass: JsonDateHttpInterceptor,
                multi: true
            }
        ]
    }
    )

export class AppModule
{

}
