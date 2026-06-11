using RestSharp;

namespace Apps.Smartsheet.Api.Requests;

public class SmartsheetRequest : RestRequest
{
    public SmartsheetRequest(
        string endpoint, 
        Method method = Method.Get, 
        string apiVersion = "2.0") : base($"{apiVersion}/{endpoint.TrimStart('/')}", method)
    {
        this.AddHeader("smartsheet-integration-source", "APPLICATION,Blackbird,SmartsheetApp");
    }
}