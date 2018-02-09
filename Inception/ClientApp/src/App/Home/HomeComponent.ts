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
    public SiteTestResult: SiteTestResult;
    public TestedSiteDomainNames: string[];
    public SearchControl: FormControl = new FormControl();
    public filteredDomainNames: Observable<string[]>;

    constructor(private _testingService: TestingService, private _domainNameService: DomainNameService)
    {

    }

    public ngOnInit(): void
    {
        this.TestedSiteDomainNames = this._domainNameService.GetTestedSiteDomainNames("");

        this.filteredDomainNames = this.SearchControl.valueChanges.pipe
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
}
