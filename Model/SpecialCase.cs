using CustomsController.Model;

/// <summary>
/// Database model for postal codes
/// </summary>
public class SpecialCase
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="Country">Country's A2 isocode</param>
    /// <param name="Code">Postal code</param>
    /// <param name="Type">Type of the postal code: region or postal code</param>
    public SpecialCase(string Code, SpecialCaseType Type)
    {
        this.Code = Code;
        this.Type = Type;
    }

    /// <summary>
    /// AutoIncrement-ID of the config
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Postal code 
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// City
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Type of the postal code: region or postal code
    /// </summary>
    public SpecialCaseType Type { get; set; }

    /// <summary>
    /// Country's ID
    /// </summary>
    public int CountryID { get; set; }

    /// <summary>
    /// Which country does this postal code belong to
    /// </summary>
    public Country Country { get; set; }
}