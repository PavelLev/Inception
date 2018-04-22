import { AppModule } from "./App/AppModule";
import { enableProdMode, NgModuleRef } from "@angular/core";
import { Environment } from "./Environments/Environment";
import { HmrBootstrap } from "./HmrBootstrap";
import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";

(() =>
{
    function getBaseUrl(): string
    {
        return document.getElementsByTagName("base")[0].href;
    }

    const providers = [
        { provide: "BASE_URL", useFactory: getBaseUrl, deps: [] }
    ];

    if (Environment.production)
    {
        enableProdMode();
    }

    const bootstrap: () => Promise<NgModuleRef<AppModule>> = () => platformBrowserDynamic(providers).bootstrapModule(AppModule);

    if (Environment.hmr)
    {
        if (module["hot"])
        {
            HmrBootstrap(module, bootstrap);
        }
        else
        {
            console.error("HMR is not enabled for webpack-dev-server!");
            console.log("Are you using the --hmr flag for ng serve?");
        }
    }
    else
    {
        bootstrap()
            .catch(err => console.log(err));
    }
})();

