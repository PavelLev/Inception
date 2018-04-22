import { Component, OnInit } from "@angular/core";
import { SiteTestOverviewService } from "./SiteTestOverviewService";
import { SiteTestOverview } from "./SiteTestOverview";
import { Observable } from "rxjs/Observable";
import { ActivatedRoute, Params } from "@angular/router";

@Component
    (
    {
        selector: "SiteTestOverview",
        styleUrls: ["SiteTestOverviewComponent.css"],
        templateUrl: "SiteTestOverviewComponent.html"
    }
    )
export class SiteTestOverviewComponent implements OnInit
{
    public DomainName: string = "";
    public SiteTestOverview: SiteTestOverview;
    
    constructor(
        private _siteTestOverviewService: SiteTestOverviewService,
        private _activatedRoute: ActivatedRoute
    )
    {
        this.DomainName =  this._activatedRoute.snapshot.params.domain;
    }

    public ngOnInit(): void
    {

        this._siteTestOverviewService.GetSiteTestOverview(this.DomainName)
            .subscribe
            (
            siteTestOverview => this.SiteTestOverview = siteTestOverview
            );
    }
}
