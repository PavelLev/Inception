import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs/BehaviorSubject";

@Injectable()
export class OverlaySettingsService 
{
    private _isOverlayShown = new BehaviorSubject<boolean>(false);
    public IsOverlayShown = this._isOverlayShown.asObservable();
    
    constructor()
    {
    }

    public changeOverlaySetting(setting: boolean) 
    {
        this._isOverlayShown.next(setting)
    }
}