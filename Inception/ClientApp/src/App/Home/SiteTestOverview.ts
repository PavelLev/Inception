import { LinkTestOverview } from "./LinkTestOverview";

export class SiteTestOverview
{
    public domainName: string;
    public linkTestOverviews: LinkTestOverview[];
    public firstTestedOn: Date;
    public lastTestedOn: Date;
}
