import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
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

    public GetSiteTestResultThumbnails(domainName: string): Observable<SiteTestResultThumbnail[]>
    {
        let Params = 
        {
            "domainName" : domainName
        };

        let Url = "api/SiteTestResult/GetSiteTestResultThumbnails";

        return this.httpClient.post<SiteTestResultThumbnail[]>(Url, Params, this._httpClientOptions);
    }
}
