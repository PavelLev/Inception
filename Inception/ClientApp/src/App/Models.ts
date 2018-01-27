export class SiteTestResult
{
    public Id: string;
    public DomainName: string;
    public LinkTestResults: LinkTestResult[];
    public TestedOn: Date;
}

export class LinkTestResult
{
    public Id: string;
    public ResponseTime: number;
    public Url: string;
}
