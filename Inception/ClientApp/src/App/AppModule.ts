import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './AppComponent';
import { NavigationMenuComponent } from './NavigationMenu/NavigationMenuComponent';
import { HomeComponent } from './Home/HomeComponent';
import { AppRouting } from './AppRouting';
import { AlertModule, ButtonsModule } from 'ngx-bootstrap';


@NgModule({
    declarations: [
        AppComponent,
        NavigationMenuComponent,
        HomeComponent,
    ],
    imports: [
        AlertModule.forRoot(), 
        ButtonsModule.forRoot(),
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        AppRouting,
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
