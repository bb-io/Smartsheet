using Apps.Smartsheet.Models.Entities.User;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Smartsheet.Models.Response.User;

public record UserResponse
{
    public UserResponse(UserEntity user)
    {
        Id = user.Id;
        Email = user.Email;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Status = user.Status;
        IsAdmin = user.IsAdmin;
        IsGroupAdmin = user.IsGroupAdmin;
        IsInternal = user.IsInternal;
    }

    [Display("User ID")] 
    public string Id { get; set; }

    [Display("User email")]
    public string Email { get; set; }

    [Display("User first name")]
    public string FirstName { get; set; }

    [Display("User last name")]
    public string LastName { get; set; }

    [Display("User status")] 
    public string Status { get; set; }

    [Display("Is admin")]
    public bool IsAdmin { get; set; }

    [Display("Is group admin")]
    public bool IsGroupAdmin { get; set; }

    [Display("Is internal")]
    public bool IsInternal { get; set; }
}