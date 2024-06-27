/// <summary>
/// Custom response for CustomsController 
/// </summary>
public class CustomsResponse
{
    /// <summary>
    /// Is country in EUCU?
    /// </summary>
    public bool? IfCustomsInEUCU { get; set; }
    /// <summary>
    /// Were there any exceptions? true = no, false = yes
    /// </summary>
    public bool Success { get; set; }
    /// <summary>
    /// Comment on response
    /// </summary>
    public string? Message { get; set; }
}