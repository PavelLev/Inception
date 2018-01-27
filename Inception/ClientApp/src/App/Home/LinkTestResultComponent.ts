import { Component, OnInit, Input } from "@angular/core";
import { LinkTestResult } from "../Models";

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
    public linkTestResult: LinkTestResult;

    constructor()
    {

    }

    public ngOnInit()
    {
        console.log(this.linkTestResult);
    }
}
