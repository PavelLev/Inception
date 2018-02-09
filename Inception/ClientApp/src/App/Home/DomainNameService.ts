import { Injectable } from "@angular/core";

@Injectable()
export class DomainNameService
{

    public GetTestedSiteDomainNames(filter: string): string[]
    {
        return TestedDomainNames;
    }

}

const TestedDomainNames =
[
    "www.hys-enterprise.com",
    "yarnpkg.com",
    "www.deezer.com",
    "material.angular.io",
    "www.google.com.ua",
    "trello.com",
    "stackoverflow.com",
    "stackoverflow.com"
];
