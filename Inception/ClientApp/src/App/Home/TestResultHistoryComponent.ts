import { Component, OnInit, Input } from "@angular/core";
import { TestingService } from "../Services";
import { ActivatedRoute } from "@angular/router";
import { SiteTestResult } from "./SiteTestResult";

@Component
    (
    {
        selector: "TestResultHistory",
        styleUrls: ["TestResultHistoryComponent.css"],
        templateUrl: "TestResultHistoryComponent.html"
    }
    )
export class TestResultHistoryComponent implements OnInit
{
    @Input()
    public SiteTestResult: SiteTestResult;

    constructor(private _testingService: TestingService, private route: ActivatedRoute)
    {

    }

    public ngOnInit()
    {
        this.SiteTestResult = this._testingService.GetSiteTestResult(this.route.snapshot.params["id"]);
        console.log(this.SiteTestResult);
    }
}
