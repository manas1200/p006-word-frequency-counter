using System.ComponentModel.DataAnnotations;
using CLOOPS.NATS.Messages;

/// <summary>
/// Health reply message containing status information.
/// </summary>
public class HealthReply : BaseMessage
{
    /// <summary>
    /// Dictionary containing health status information.
    /// Keys typically include: appName, appStatus, appMessage, httpResponse, responder.
    /// </summary>
    [Required(ErrorMessage = "Status is required")]
    [MinLength(1, ErrorMessage = "Status must contain at least one entry")]
    public Dictionary<string, string> Status { get; set; } = new();
}

