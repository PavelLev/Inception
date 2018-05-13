import { Injectable } from "@angular/core";
import { Resolve } from "@angular/router";
import { TestingService } from "./TestingService";
import { SiteTestResult } from "./Home/SiteTestResult";
import { Observable } from "rxjs";

@Injectable()
export class Resolver implements Resolve<any>
{
    constructor(private _testingService: TestingService)
    {

    }

    public resolve(): Observable<SiteTestResult>
    {
        return this._testingService.GetSiteTestResult("1");
    }


}
