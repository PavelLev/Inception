import { CanActivate, ActivatedRouteSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class SiteTestOverviewComponentActivator implements CanActivate
{
    public canActivate(route: ActivatedRouteSnapshot): boolean
    {
       let canActivateRoute = route.params["DomainName"] === undefined ? true : false;
       return  canActivateRoute;
    }

}
