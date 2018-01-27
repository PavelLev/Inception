import { OnInit, Component } from "@angular/core";
import { TestingService } from "../Services";
import { SiteTestResult } from "./SiteTestResult";

@Component
    (
    {
        selector: "Home",
        templateUrl: "./HomeComponent.html",
    }
    )

export class HomeComponent implements OnInit
{
    public SiteTestResult: SiteTestResult;

    constructor(private _testingService: TestingService)
    {

    }

    public ngOnInit(): void
    {

    }

    public GetTestResultHistoryList()
    {
        this.SiteTestResult = this._testingService.GetSiteTestResult("1");
    }
}
