import { OnInit, Component } from "@angular/core";
import { TestingService } from "../Services";
import { SiteTestResult } from "./SiteTestResult";
import { DomainNameService } from "./DomainNameService";
import { FormControl } from "@angular/forms";
import { Observable } from "rxjs/Observable";
import {startWith} from "rxjs/operators/startWith";
import { map } from "rxjs/operator/map";
import { GlobalService } from "../GlobalService";

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

    constructor(private _testingService: TestingService, private _domainNameService: DomainNameService, private _globalService: GlobalService)
    {

    }

    public ngOnInit(): void
    {
        this._globalService.currentMessage.subscribe(message => this.IsOverlayDark = message);

        this.TestedSiteDomainNames = this._domainNameService.GetTestedSiteDomainNames("");

        this.FilteredDomainNames = this.SearchControl.valueChanges.pipe
        (
            startWith(""),
            x => x.map
            (
                val => this.filter(val)
            )
        );
    }

    public filter(val: string): string[]
    {
        return this.TestedSiteDomainNames.filter
        (
            name =>
            name.toLowerCase().indexOf(val.toLowerCase()) === 0
        );
    }

    public ShowTestResultHistoryList(): void
    {
        this.SiteTestResult = this._testingService.GetSiteTestResult("1");
    }

    public SetDarkOverlay(): void
    {
        this._globalService.changeMessage(true);
    }

    public SetOverlayDependedOnKeyPressed(event): void
    {
        if(event.keyCode == 13)
        {
            this.SetLightOverlay();
        } 
        if(event.inputType == "deleteContentBackward")
        {
            this.SetDarkOverlay();
        }
    }
    
    public SetLightOverlay(): void
    {        
        this._globalService.changeMessage(false);
    }

}
