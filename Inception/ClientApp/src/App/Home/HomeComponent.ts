import { OnInit, Component } from "@angular/core";
import { TestingService } from "../Services";
import { SiteTestResult } from "./SiteTestResult";
import { DomainNameService } from "./DomainNameService";
import { FormControl } from "@angular/forms";
import { Observable } from "rxjs/Observable";
import {startWith} from "rxjs/operators/startWith";
import { map } from "rxjs/operator/map";

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
    public DomainName: string;
    public SiteTestResult: SiteTestResult;
    public TestedSiteDomainNames: string[];
    public SearchControl: FormControl = new FormControl();
    public FilteredDomainNames: Observable<string[]>;

    constructor(private _testingService: TestingService, private _domainNameService: DomainNameService)
    {

    }

    public ngOnInit(): void
    {
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
        console.log(this.DomainName);
        this.SiteTestResult = this._testingService.GetSiteTestResult("1");
    }

    public CallSomeFunction(name: string): void
    {        
        console.log("2: " + this.DomainName)
        console.log("1: " + name)
    }
}
