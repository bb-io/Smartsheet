using RestSharp;

namespace Apps.Smartsheet.Api.Requests;

public class SmartsheetRequest(string endpoint, Method method = Method.Get, string apiVersion = "2.0")
    : RestRequest($"{apiVersion}/{endpoint.TrimStart('/')}", method);