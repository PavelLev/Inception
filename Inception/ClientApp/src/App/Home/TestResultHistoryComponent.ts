import { Component, OnInit, Input } from "@angular/core";
import { SiteTestResult } from "../Models";
import { Service } from "../Services";
import { ActivatedRoute } from "@angular/router";

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

    constructor(private service: Service, private route: ActivatedRoute)
    {

    }

    public ngOnInit()
    {
        this.SiteTestResult = this.service.getSiteTestResult(this.route.snapshot.params["id"]);
        console.log(this.SiteTestResult);
    }
}
