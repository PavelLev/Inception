import { Pipe, PipeTransform } from "@angular/core";

@Pipe
    (
    {
        name: "RemoveScheme"
    }
    )
export class RemoveScheme implements PipeTransform
{
    transform(value: string): string {

        return value.includes("//") 
                ? value.split("//")[1] 
                : value;

      }
}