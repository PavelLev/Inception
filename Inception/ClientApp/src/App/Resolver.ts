import { Injectable } from "@angular/core";
import { Resolve } from "@angular/router";
import { TestingService } from "./TestingService";
import { SiteTestResult } from "./Home/SiteTestResult";

@Injectable()
export class Resolver implements Resolve<any>
{
    constructor(private _testingService: TestingService)
    {

    }

    public resolve(): SiteTestResult
    {
        return this._testingService.GetSiteTestResult("1");
    }


}
