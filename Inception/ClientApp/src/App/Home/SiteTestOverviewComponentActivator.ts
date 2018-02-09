import { CanActivate, ActivatedRouteSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";

@Injectable()
export class SiteTestOverviewComponentActivator implements CanActivate
{
    public canActivate(route: ActivatedRouteSnapshot): boolean
    {
       return false; // return  canActivateRoute = route.params["DomainName"] === undefined ? true : false;
    }

}
