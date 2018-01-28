import { Component, OnInit, Input } from "@angular/core";
import { LinkTestResult } from "./LinkTestResult";

@Component
    (
    {
        selector: "LinkTestResult",
        styleUrls: ["LinkTestResultComponent.css"],
        templateUrl: "LinkTestResultComponent.html"
    }
    )
export class LinkTestResultComponent implements OnInit
{
    @Input()
    public LinkTestResult: LinkTestResult;

    constructor()
    {

    }

    public ngOnInit(): void
    {
        console.log(this.LinkTestResult);
    }
}
