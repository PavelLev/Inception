import { LinkTestResult } from "./LinkTestResult";

export class SiteTestResult
{
    public Id: string;
    public DomainName: string;
    public LinkTestResults: LinkTestResult[];
    public TestedOn: Date;
}
