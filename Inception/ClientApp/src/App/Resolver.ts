import { Injectable } from "@angular/core";
import { Resolve } from "@angular/router";
import { Service } from "./Services";

@Injectable()
export class Resolver implements Resolve<any>
{
    constructor(public service: Service)
    {

    }

    public resolve()
    {
        return this.service.getSiteTestResult("1");
    }


}
