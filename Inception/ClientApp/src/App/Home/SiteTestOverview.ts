import { LinkTestOverview } from "./LinkTestOverview";

export class SiteTestOverview
{
    public Id: string;
    public DomainName: string;
    public LinkTestOverviews: LinkTestOverview[];
    public FirstTestedOn: Date;
    public LastTestedOn: Date;
}
