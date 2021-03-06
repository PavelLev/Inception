import { Component, OnInit, Input } from "@angular/core";
import { TestingService } from "../TestingService";
import { ActivatedRoute } from "@angular/router";
import { SiteTestResultThumbnail } from "./SiteTestResultThumbnail";

@Component
    (
    {
        selector: "TestResultHistoryList",
        styleUrls:
        [
            "TestResultHistoryListComponent.scss"
        ],
        templateUrl: "TestResultHistoryListComponent.html"
    }
    )

export class TestResultHistoryListComponent implements OnInit
{
    @Input()
    public SiteTestResultThumbnail: SiteTestResultThumbnail;

    constructor(private _testingService: TestingService, private _activatedRoute: ActivatedRoute)
    {

    }



    public ngOnInit(): void
    {

    }
}


