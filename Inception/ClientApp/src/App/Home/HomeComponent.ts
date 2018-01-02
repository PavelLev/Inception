import { OnInit, Component } from "@angular/core";

@Component(
    {
        selector: "Home",
        templateUrl: "./HomeComponent.html",
    }
)
export class HomeComponent implements OnInit 
{
    public ngOnInit(): void 
    {
        console.log("Home onInit");
    }
}
