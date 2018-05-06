import { Component, OnInit, Input, OnChanges, SimpleChanges } from "@angular/core";
import { SiteTestOverview } from "../../SiteTestOverview";

@Component({
    selector: "BarChart",
    templateUrl: "./BarChartComponent.html",
    styleUrls: ["./BarChartComponent.scss"]
})
export class BarChartComponent implements OnInit, OnChanges {

    constructor() { }

    ngOnChanges(changes: SimpleChanges) 
    {
        if (this.SiteTestOverview != null) {
            this.SetBarChartLabels();
            this.SetBarChartData();
        }
    }

    ngOnInit() {

    }

    @Input()
    public SiteTestOverview: SiteTestOverview;
    
    public barChartOptions:any = {
        scaleShowVerticalLines: false,
        responsive: true
    };

    public barChartLabels:string[];
    public barChartType:string = "horizontalBar";
    public barChartLegend:boolean = true;
    
    public barChartData:any[];
    
    public SetBarChartLabels(): void {
        this.barChartLabels = this.SiteTestOverview.LinkTestOverviews.map(x => x.Url);
    }

    public SetBarChartData(): void {

        this.barChartData = [
            { 
                data: this.SiteTestOverview.LinkTestOverviews.map(x => x.MinimumResponseTime),
                label: "Min"
            },
            { 
                data: this.SiteTestOverview.LinkTestOverviews.map(x => x.MaximumResponseTime),
                label: "Max"
            }
        ];
    }
    // events
    public chartClicked(e:any):void {
    console.log(e);console.log(this.SiteTestOverview.LinkTestOverviews);
    }
    
    public chartHovered(e:any):void {
    console.log(e);
    
    }
    
    public randomize():void {
    // Only Change 3 values
    let data = [
        Math.round(Math.random() * 100),
        59,
        80,
        (Math.random() * 100),
        56,
        (Math.random() * 100),
        40];
    let clone = JSON.parse(JSON.stringify(this.barChartData));
    clone[0].data = data;
    this.barChartData = clone;
    /**
     * (My guess), for Angular to recognize the change in the dataset
     * it has to change the dataset variable directly,
     * so one way around it, is to clone the data, change it and then
     * assign it;
     */
    }

}
