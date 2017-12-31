import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { environment } from './Environments/environment';
import { HmrBootstrap } from './HmrBootstrap';
import { AppModule } from './App/AppModule';


export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}

const providers = [
    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
    enableProdMode();
}

const bootstrap = () => platformBrowserDynamic(providers).bootstrapModule(AppModule);

if (environment.hmr) 
{
    if (module[ 'hot' ]) 
    {
        HmrBootstrap(module, bootstrap);
    } 
    else 
    {
        console.error('HMR is not enabled for webpack-dev-server!');
        console.log('Are you using the --hmr flag for ng serve?');
    }
} 
else 
{
    bootstrap()
        .catch(err => console.log(err));
}
