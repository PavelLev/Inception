import { Component } from '@angular/core';

@Component({
    selector: 'NavigationMenu',
    templateUrl: './NavigationMenuComponent.html',
    styleUrls: ['./NavigationMenuComponent.css']
})
export class NavigationMenuComponent {
    private _isExpanded = false;

    collapse() {
        this._isExpanded = false;
    }

    toggle() {
        this._isExpanded = !this._isExpanded;
    }
}
