import { OnInit, Component } from "@angular/core";
import { TestingService } from "../Services";
import { SiteTestResult } from "./SiteTestResult";
import { DomainNameService } from "./DomainNameService";
import { FormControl } from "@angular/forms";
import { Observable } from "rxjs/Observable";
import {startWith, map} from "rxjs/operators";
import { OverlaySettingsService } from "../OverlaySettingsService";

@Component
    (
    {
        selector: "Home",
        styleUrls:
        [
            "HomeController.css"
        ],
        templateUrl: "./HomeComponent.html",
    }
    )

export class HomeComponent implements OnInit
{
    public IsOverlayDark: boolean = false;
    public DomainName: string;
    public SiteTestResult: SiteTestResult;
    public TestedSiteDomainNames: string[];
    public SearchControl: FormControl = new FormControl();
    public FilteredDomainNames: Observable<string[]>;

    constructor(private _testingService: TestingService, private _domainNameService: DomainNameService, private _overlaySettingsService: OverlaySettingsService)
    {

    }

    public ngOnInit(): void
    {
        this._overlaySettingsService.currentMessage.subscribe(message => this.IsOverlayDark = message);

        this._domainNameService.GetTestedSiteDomainNames("")
        .subscribe
            (
            x => 
            {
                this.TestedSiteDomainNames = x;
                
                this.FilteredDomainNames = this.SearchControl.valueChanges.pipe
                (
                    startWith(""),
                    map
                    (
                        val => this.filter(val)
                    )
                );
            }            
            );
    }

    public filter(val: string): string[]
    {
        return this.TestedSiteDomainNames.filter
            (
            name =>
                name.toLowerCase().startsWith(val.toLowerCase())
            );
    }

    public ShowTestResultHistoryList(): void
    {
        this.SiteTestResult = this._testingService.GetSiteTestResult("1");
    }

    public HideOverlay(): void
    {
        this._overlaySettingsService.changeMessage(true);
    }

    public SetOverlayOnKeyPressed(event): void
    {
        if(event.keyCode == 13)
        {
            this.ShowOverlay();
        } 
        if(event.inputType == "deleteContentBackward")
        {
            this.HideOverlay();
        }
    }
    
    public ShowOverlay(): void
    {        
        this._overlaySettingsService.changeMessage(false);
    }

}
