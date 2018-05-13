import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { HttpClient, HttpParams, HttpHeaders } from "@angular/common/http";

@Injectable()
export class DomainNameService
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

    public GetTestedSiteDomainNames(filter: string): Observable<string[]>
    {
        let Params = 
        {
            "filter" : filter
        };

        let Url = "api/DomainName/GetTestedSiteDomainNames";

        return this.httpClient.post<string[]>(Url, Params, this._httpClientOptions);
    }
}
