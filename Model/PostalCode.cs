using CustomsController.Model;


/// <summary>
/// Database model for postal codes
/// </summary>
public class PostalCode
{

    /// <summary>
    /// Costructor
    /// </summary>
    /// <param name="Country">Country's A2 isocode</param>
    /// <param name="Code">Postal code</param>
    /// <param name="Type">Type of the postal code: region or postal code</param>
    public PostalCode(string Code, PostalCodeType Type)
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
    /// Type of the postal code: region or postal code
    /// </summary>
    public PostalCodeType Type { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int CountryID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Country Country { get; set; }
}