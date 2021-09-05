import { JsonObject, JsonProperty, Any } from "json2typescript";


export class ShipResponse {
    @JsonProperty("succeeded", Boolean)
    succeeded?: boolean = undefined;
   
    @JsonProperty("data", Any)
    data?: any = undefined;
    
    @JsonProperty("errors", Any)
    errors?: any = undefined;
}