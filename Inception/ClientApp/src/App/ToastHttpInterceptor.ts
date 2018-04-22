import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpResponse, HttpErrorResponse } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import { ToastrService } from 'ngx-toastr';
import { BusinessException } from './BusinessException';

@Injectable()
export class ToastHttpInterceptor implements HttpInterceptor
{
    constructor(private _toastrService: ToastrService)
    {

    }

    public intercept
        (
        httpRequest: HttpRequest<any>,
        next: HttpHandler
        ): Observable<HttpEvent<any>>
    {

        return next.handle(httpRequest)
            .do
            (
            httpEvent => 
            {
                
            },
            response =>
            {
                console.log(response);

                if (response instanceof HttpErrorResponse)
                {
                    if (response.status === 400)
                    {
                        console.log(response.error);
                        this._toastrService.error(response.error.Description, "Error")
                    }
                    else
                    {
                        this._toastrService.error("Something went wrong", "Error");
                    }
                }
            }
            );

    }
}