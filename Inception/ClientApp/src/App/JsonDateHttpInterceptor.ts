// inspired by https://stackoverflow.com/questions/46559268/parse-date-with-angular-4-3-httpclient

import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

// https://github.com/angular/angular/blob/master/packages/common/http/src/xhr.ts#L18
const XSSI_PREFIX = /^\)\]\}',?\n/;

@Injectable()
export class JsonDateHttpInterceptor implements HttpInterceptor
{
    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>
    {
        if (request.responseType !== "json")
        {
            return next.handle(request);
        }

        // convert to responseType of text to skip angular parsing
        request = request.clone
            (
            {
                responseType: "text"
            }
            );

        return next.handle(request)
            .pipe
            (
            map
                (
                event =>
                {
                    if (!(event instanceof HttpResponse))
                    {
                        return event;
                    }

                    return this.processJsonResponse(event);
                }
                )
            );
    }



    private processJsonResponse(response: HttpResponse<string>): HttpResponse<any>
    {
        let body = response.body;
    
        if (typeof body === "string")
        {
            const originalBody = body;

            body = body.replace(XSSI_PREFIX, "");

            try
            {
                body = body !== ""
                    ?
                    JSON.parse
                        (
                        body,
                        this.reviveUtcDate
                        )
                    :
                    null;
            }
            catch (error)
            {
                // match https://github.com/angular/angular/blob/master/packages/common/http/src/xhr.ts#L221
                throw new HttpErrorResponse
                    (
                    {
                        error:
                        {
                            error,
                            text: originalBody
                        },
                        headers: response.headers,
                        status: response.status,
                        statusText: response.statusText,
                        url: response.url || undefined,
                    }
                    );
            }
        }

        return response.clone({ body });
    }



    private reviveUtcDate(_, value: any): any
    {
        if (typeof value !== "string")
        {
            return value;
        }

        if (value === "0001-01-01T00:00:00")
        {
            return null;
        }

        const match = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*)?)$/.exec(value);

        if (!match)
        {
            return value;
        }

        return new Date(value);
    }
}