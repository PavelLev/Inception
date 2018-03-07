import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";
import { HttpClient, HttpParams, HttpHeaders } from "@angular/common/http";
import { SiteTestResultThumbnail } from "./SiteTestResultThumbnail";

@Injectable()
export class SiteTestResultService
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

    constructor(private httpClient: HttpClient) { }

    public GetSiteResults(domainName: string): Observable<SiteTestResultThumbnail[]>
    {
        let Params = 
        {
            "domainName" : domainName
        };

        let Url = "api/SiteTestResult/GetSiteResults";

        return this.httpClient.post<SiteTestResultThumbnail[]>(Url, Params, this._httpClientOptions);
    }
}
