import { Component } from "@angular/core";

@Component
    (
    {
        selector: "NavigationMenu",
        styleUrls: ["./NavigationMenuComponent.css"],
        templateUrl: "./NavigationMenuComponent.html",
    }
    )
export class NavigationMenuComponent {
    private _isExpanded = false;

    public collapse()
    {
        this._isExpanded = false;
    }

    private toggle()
    {
        this._isExpanded = !this._isExpanded;
        if (true)
        {

        }
    }
}
