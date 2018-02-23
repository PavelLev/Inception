import { Component, OnInit } from "@angular/core";
import { OverlaySettingsService } from "./OverlaySettingsService";

@Component
    (
    {
        selector: "App",
        styleUrls: 
        [
            "./AppComponent.css"
        ],
        templateUrl: "./AppComponent.html"
    }
    )

export class AppComponent implements OnInit
{
    public IsOverlayDark: boolean;
    public title: string = "app";

    constructor(private _overlaySettingsService: OverlaySettingsService)
    {

    }

    ngOnInit(): void 
    {
        this._overlaySettingsService.currentMessage.subscribe(message => this.IsOverlayDark = message);
    }
}
    
