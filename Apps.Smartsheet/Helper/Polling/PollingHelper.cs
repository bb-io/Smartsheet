using Apps.Smartsheet.Polling.Models.Memory;
using Blackbird.Applications.Sdk.Common.Polling;

namespace Apps.Smartsheet.Helper.Polling;

public static class PollingHelper
{
    public static PollingEventResponse<DateMemory, T> DontFlyBird<T>(DateTime lastInteractionTimestamp)
    {
        return new PollingEventResponse<DateMemory, T>
        {
            FlyBird = false,
            Memory = new DateMemory { LastInteraction = lastInteractionTimestamp }
        };
    }
    
    public static PollingEventResponse<DateMemory, T> FlyBird<T>(T result, DateTime lastInteractionTimestamp)
    {
        return new PollingEventResponse<DateMemory, T>
        {
            FlyBird = true,
            Memory = new DateMemory { LastInteraction = lastInteractionTimestamp },
            Result = result
        };
    }
}