import { HttpClient, HttpHeaders } from "@angular/common/http";
import { SiteTestOverview } from "./SiteTestOverview";
import { Observable } from "rxjs/Observable";
import { Injectable } from "@angular/core";

@Injectable()
export class SiteTestOverviewService
{
    private _httpClientOptions = 
    {
        headers: new HttpHeaders
        (
        {
          "Content-Type" : "application/json"
        }
        )
    };
    
    constructor(private httpClient: HttpClient){}

    public GetSiteTestOverview(domainName: string): Observable<SiteTestOverview>
    {
        let Params = 
        {
            "domainName" : domainName
        };

        let Url = "api/SiteTestOverview/GetSiteTestOverview";

        return this.httpClient.post<SiteTestOverview>(Url, Params, this._httpClientOptions);
    }
}
