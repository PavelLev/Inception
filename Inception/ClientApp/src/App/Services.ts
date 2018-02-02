import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";
import { Injectable } from "@angular/core";
import { LinkTestResult } from "./Home/LinkTestResult";
import { SiteTestResult } from "./Home/SiteTestResult";

@Injectable()

export class TestingService {

    public getLinkTestResults(): Observable<LinkTestResult[]>
    {
        let subject: Subject<LinkTestResult[]>;

        subject = new Subject<LinkTestResult[]>();
        setTimeout
            (
            () =>
            {
                subject.next(LinkTestResults);
                subject.complete();
            },
            100
            );

        return subject;
    }

    public GetSiteTestResult(id: string): SiteTestResult
    {
        return TestSiteTestResult.find(x => x.Id === id);
    }
}

const TestSiteTestResult: SiteTestResult[] =
[
    {
        DomainName: "www.hys-enterprise.com",
        Id: "1",
        LinkTestResults: 
        [
            {
                Id: "1",
                ResponseTime: 0.2,
                Url: "http://www.hys-enterprise.com/"
            },
            {
                Id: "2",
                ResponseTime: 0.4,
                Url: "http://www.hys-enterprise.com/Home/Telecoms",
            },
            {
                Id: "3",
                ResponseTime: 0.4,
                Url: "http://www.hys-enterprise.com/Home/LBS"
            },
            {
                Id: "4",
                ResponseTime: 0.4,
                Url: "http://www.hys-enterprise.com/Home/Finance"
            },
            {
                Id: "5",
                ResponseTime: 0.4,
                Url: "http://www.hys-enterprise.com/Home/Development"
            },
        ],
        TestedOn: new Date()
    },
    {
        DomainName: "dou.ua",
        Id: "2",
        LinkTestResults: 
        [
            {
                Id: "1",
                ResponseTime: 0.2,
                Url: "https://dou.ua/"
            },
            {
                Id: "2",
                ResponseTime: 0.4,
                Url: "https://dou.ua/Home/Telecoms",
            },
            {
                Id: "3",
                ResponseTime: 0.4,
                Url: "https://dou.ua/Home/LBS"
            },
            {
                Id: "4",
                ResponseTime: 0.4,
                Url: "https://dou.ua/Home/Finance"
            },
            {
                Id: "5",
                ResponseTime: 0.4,
                Url: "https://dou.ua/Home/Development"
            },
        ],
        TestedOn: new Date()
    }
];

const LinkTestResults: LinkTestResult[] =
[
    {
        Id: "1",
        ResponseTime: 0.2,
        Url: "http://www.hys-enterprise.com/"
    },
    {
        Id: "2",
        ResponseTime: 0.4,
        Url: "http://www.hys-enterprise.com/Home/Telecoms",
    },
    {
        Id: "3",
        ResponseTime: 0.4,
        Url: "http://www.hys-enterprise.com/Home/LBS"
    },
    {
        Id: "4",
        ResponseTime: 0.4,
        Url: "http://www.hys-enterprise.com/Home/Finance"
    },
    {
        Id: "5",
        ResponseTime: 0.4,
        Url: "http://www.hys-enterprise.com/Home/Development"
    },
];
