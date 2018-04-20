import { ToastrService } from "ngx-toastr";
import { ErrorHandler, Injectable, Injector } from "@angular/core";

@Injectable()
export class ToastErrorHandler extends ErrorHandler 
{
    private _toastrService: ToastrService

    private get ToastrService(): ToastrService
    {
        if (this._toastrService == null)
        {
            this._toastrService = this._injector.get<ToastrService>(ToastrService);
        }

        return this._toastrService;
    }

    constructor(private _injector: Injector) 
    {
        super();
    }

    handleError(error: any)
    {
        this.ToastrService.error("Something went wrong", "Error");

        super.handleError(error);
    }
}