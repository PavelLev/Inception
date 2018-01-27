import { Injectable } from "@angular/core";
import { Resolve } from "@angular/router";
import { TestingService } from "./Services";

@Injectable()
export class Resolver implements Resolve<any>
{
    constructor(private _testingService: TestingService)
    {

    }

    public resolve()
    {
        return this._testingService.GetSiteTestResult("1");
    }


}
