import { Component, OnInit } from "@angular/core";
import { GlobalService } from "./GlobalService";

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

    constructor(private _globalService: GlobalService)
    {

    }

    ngOnInit(): void 
    {
        this._globalService.currentMessage.subscribe(message => this.IsOverlayDark = message);
    }
}
    
