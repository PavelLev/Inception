import { LinkTestOverview } from "./LinkTestOverview";

export class SiteTestOverview
{
    public DomainName: string;
    public LinkTestOverviews: LinkTestOverview[];
    public FirstTestedOn: Date;
    public LastTestedOn: Date;
}
