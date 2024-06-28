/// <summary>
/// Database model for countries
/// </summary>
public class Country
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="A2Code">Country's A2 isocode</param>
    /// <param name="IsEUCU">Is it a member of EUCU</param>
    public Country(string A2Code, bool IsEUCU)
    {
        this.A2Code = A2Code;
        this.IsEUCU = IsEUCU;
    }

    /// <summary>
    /// AutoIncrement-ID of the config
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Country's A2 isocode
    /// </summary>
    public string A2Code { get; set; } //e.g. "DE", "PL"

    /// <summary>
    /// Is it a member of EUCU
    /// </summary>
    public bool IsEUCU { get; set; }

    /// <summary>
    /// Special cases of the country
    /// </summary>
    public ICollection<SpecialCase> PostalCodes { get; set; }
}