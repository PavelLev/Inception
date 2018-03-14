import { OnInit, Component } from "@angular/core";
import { TestingService } from "../TestingService";
import { SiteTestResult } from "./SiteTestResult";
import { DomainNameService } from "./DomainNameService";
import { FormControl } from "@angular/forms";
import { Observable } from "rxjs/Observable";
import {startWith, map} from "rxjs/operators";
import { OverlaySettingsService } from "../OverlaySettingsService";
import { SiteTestResultService } from "./SiteTestResultService";
import { SiteTestResultThumbnail } from "./SiteTestResultThumbnail";

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
    public IsOverlayShown: boolean = false;
    public DomainName: string = "";
    public SiteTestResultThumbnails: SiteTestResultThumbnail[];
    public TestedSiteDomainNames: string[];
    public SearchControl: FormControl = new FormControl();
    public FilteredDomainNames: Observable<string[]>;

    constructor
        (
        private _testingService: TestingService, 
        private _domainNameService: DomainNameService, 
        private _overlaySettingsService: OverlaySettingsService,
        private _siteTestResultService: SiteTestResultService
        )
    {

    }

    public ngOnInit(): void
    {
        this._overlaySettingsService.IsOverlayShown.subscribe(IsOverlayShown => this.IsOverlayShown = IsOverlayShown);
        this.GetTestedSiteDomainNames();
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
        this._siteTestResultService.GetSiteTestResultThumbnails(this.DomainName).subscribe(
            SiteTestResultThumbnails => 
            {
                this.SiteTestResultThumbnails = SiteTestResultThumbnails
            }); 
    }

    public HideOverlay(): void
    {
        this._overlaySettingsService.changeOverlaySetting(true);
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
        this._overlaySettingsService.changeOverlaySetting(false);
    }

    public GetTestedSiteDomainNames(): void
    {        
        this._domainNameService.GetTestedSiteDomainNames(this.DomainName)
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
}
