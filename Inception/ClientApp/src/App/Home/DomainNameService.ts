import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";

@Injectable()
export class DomainNameService
{

    public GetTestedSiteDomainNames(filter: string): Observable<string[]>
    {
        let subject: Subject<string[]>;
        subject = new Subject<string[]>();
        setTimeout(() => subject.next(TestedDomainNames), 200);
        
        return subject;
    }

}

const TestedDomainNames  =
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
