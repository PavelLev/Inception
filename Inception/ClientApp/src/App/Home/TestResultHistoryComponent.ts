import { Component, OnInit, Input } from "@angular/core";
import { TestingService } from "../TestingService";
import { ActivatedRoute } from "@angular/router";
import { SiteTestResult } from "./SiteTestResult";

@Component
    (
    {
        selector: "TestResultHistory",
        styleUrls: 
        [
            "TestResultHistoryComponent.scss"
        ],
        templateUrl: "TestResultHistoryComponent.html"
    }
    )

export class TestResultHistoryComponent implements OnInit
{
    @Input()
    public SiteTestResult: SiteTestResult;

    constructor(private _testingService: TestingService, private _activatedRoute: ActivatedRoute)
    {

    }

    public ngOnInit(): void
    {
        this._testingService.GetSiteTestResult(this._activatedRoute.snapshot.params["id"]).subscribe(SiteTestResult => this.SiteTestResult = SiteTestResult);        
    }
}
