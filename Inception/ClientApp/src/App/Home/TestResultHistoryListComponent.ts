import { Component, OnInit, Input } from "@angular/core";
import { SiteTestResult } from "../Models";
import { Service } from "../Services";
import { ActivatedRoute } from "@angular/router";

@Component({
    selector: "TestResultHistoryList",
    templateUrl: "TestResultHistoryListComponent.html"
})

export class TestResultHistoryListComponent implements OnInit
{
    @Input()
    public SiteTestResult: SiteTestResult;

    constructor(private service: Service, private route: ActivatedRoute)
    { }



    public ngOnInit()
    {
       // this.siteTestResult = this.route.snapshot.data["siteTestResult"];
        //this.SiteTestResult = this.service.getSiteTestResult("1");
    }
}


