import { Component } from "@angular/core";

@Component
    (
    {
        selector: "NavigationMenu",
        styleUrls: ["./NavigationMenuComponent.css"],
        templateUrl: "./NavigationMenuComponent.html",
    }
    )
export class NavigationMenuComponent
{
    private _isExpanded: boolean;

    public collapse(): void
    {
        this._isExpanded = false;
    }

    private toggle(): void
    {
        this._isExpanded = !this._isExpanded;
        if (true)
        {

        }
    }
}
