import { RouteReuseStrategy, DetachedRouteHandle, ActivatedRouteSnapshot } from "@angular/router";

export class InceptionRouteReuseStrategy implements RouteReuseStrategy {
    private _handlers: {[key: string]: DetachedRouteHandle} = {};
    private _acceptedRoutes: string[] = [""];

    shouldDetach(route: ActivatedRouteSnapshot): boolean {
        let result = this._acceptedRoutes.includes(route.routeConfig.path);
        return result;
    }

    store(route: ActivatedRouteSnapshot, handle: DetachedRouteHandle): void {
        console.debug('CustomReuseStrategy:store', route, handle);
        this._handlers[route.routeConfig.path] = handle;
    }

    shouldAttach(route: ActivatedRouteSnapshot): boolean {
        console.debug('CustomReuseStrategy:shouldAttach', route);
        return !!route.routeConfig && !!this._handlers[route.routeConfig.path];
    }

    retrieve(route: ActivatedRouteSnapshot): DetachedRouteHandle {
        console.debug('CustomReuseStrategy:retrieve', route);
        if (!route.routeConfig) return null;
        return this._handlers[route.routeConfig.path];
    }

    shouldReuseRoute(future: ActivatedRouteSnapshot, curr: ActivatedRouteSnapshot): boolean {
        console.debug('CustomReuseStrategy:shouldReuseRoute', future, curr);
        return future.routeConfig === curr.routeConfig;
    }

}