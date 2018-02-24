import { Component, OnInit, Input } from "@angular/core";
import { TestingService } from "../Services";
import { ActivatedRoute } from "@angular/router";
import { SiteTestResult } from "./SiteTestResult";

@Component
    (
    {
        selector: "TestResultHistoryList",
        styleUrls:
        [
            "TestResultHistoryListComponent.css"
        ],
        templateUrl: "TestResultHistoryListComponent.html"
    }
    )

export class TestResultHistoryListComponent implements OnInit
{
    @Input()
    public SiteTestResult: SiteTestResult;

    constructor(private _testingService: TestingService, private _activatedRoute: ActivatedRoute)
    {

    }



    public ngOnInit(): void
    {

    }
}


