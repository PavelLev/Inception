import { NgModuleRef, ApplicationRef } from "@angular/core";
import { createNewHosts } from "@angularclass/hmr";

export const HmrBootstrap = (module: any, bootstrap: () => Promise<NgModuleRef<any>>) =>
{
    let ngModule: NgModuleRef<any>;
    module.hot.accept();

    bootstrap().then(mod => ngModule = mod);

    module.hot.dispose
        (
        () =>
        {
            const appRef = ngModule.injector.get(ApplicationRef);
            const elements = appRef.components.map(c => c.location.nativeElement);
            const makeVisible = createNewHosts(elements);

            ngModule.destroy();
            makeVisible();
        }
        );
};
