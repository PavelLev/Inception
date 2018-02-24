import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs/BehaviorSubject";

@Injectable()
export class OverlaySettingsService 
{
    private _isOverlayShown = new BehaviorSubject<boolean>(false);
    isOverlayShown = this._isOverlayShown.asObservable();
    
    constructor()
    {
    }

    changeOverlaySetting(setting: boolean) 
    {
        this._isOverlayShown.next(setting)
    }
}