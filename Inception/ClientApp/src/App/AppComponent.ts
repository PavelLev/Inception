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
    public IsOverlayShown: boolean;
    public title: string = "app";

    constructor(private _overlaySettingsService: OverlaySettingsService)
    {

    }

    ngOnInit(): void 
    {
        this._overlaySettingsService.IsOverlayShown.subscribe(IsOverlayShown => this.IsOverlayShown = IsOverlayShown);
    }
}
    
