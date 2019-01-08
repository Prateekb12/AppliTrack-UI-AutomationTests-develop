namespace ApplitrackUITests.DataModels
{
    public interface IAddress
    {
        string NumberAndStreet { get; set; }

        string AptNumber { get; set; }

        string City { get; set; }

        string State { get; set; }

        string Zip { get; set; }

        string Country { get; set; }

        string DaytimePhone { get; set; }

        string CellPhone { get; set; }
    }
}