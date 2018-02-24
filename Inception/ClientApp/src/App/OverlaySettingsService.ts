import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs/BehaviorSubject";

@Injectable()
export class OverlaySettingsService 
{
    public _isOverlayShown = new BehaviorSubject<boolean>(false);
    isOverlayShown = this._isOverlayShown.asObservable();
    
    constructor()
    {
    }

    public changeOverlaySetting(setting: boolean) 
    {
        this._isOverlayShown.next(setting)
    }
}