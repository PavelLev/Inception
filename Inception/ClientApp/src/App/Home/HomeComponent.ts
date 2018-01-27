import { OnInit, Component } from "@angular/core";
import { Service } from "../Services";
import { SiteTestResult } from "../Models";

@Component(
    {
        selector: "Home",
        templateUrl: "./HomeComponent.html",
    }
)
export class HomeComponent implements OnInit
{
    public SiteTestResult: SiteTestResult;

    constructor(private service: Service)
    {}

    public ngOnInit(): void
    {}

    public GetTestResultHistoryList()
    {
        this.SiteTestResult = this.service.getSiteTestResult("1");
    }
}
