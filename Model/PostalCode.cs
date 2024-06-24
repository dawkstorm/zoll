/// <summary>
/// Database model for postal codes
/// </summary>
public class PostalCode
{
    /// <summary>
    /// AutoIncrement-ID of the config
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Country's A2 isocode
    /// </summary>
    public string Country { get; set; } //e.g. "DE", "PL"

    /// <summary>
    /// Postal code 
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// Type of the postal code: region or postal code
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Costructor
    /// </summary>
    /// <param name="Country">Country's A2 isocode</param>
    /// <param name="Code">Postal code</param>
    /// <param name="Type">Type of the postal code: region or postal code</param>
    public PostalCode(string Country, string Code, string Type)
    {
        this.Country = Country;
        this.Code = Code;
        this.Type = Type;
    }
}