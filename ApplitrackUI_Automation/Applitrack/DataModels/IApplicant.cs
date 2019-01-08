namespace ApplitrackUITests.DataModels
{
    public interface IApplicant : IPerson
    {
        int AppNo { get; set; }

        string MiddleInitial { get; set; }

        string SocialSecurityNumber { get; set; }

        string SecretAnswer { get; set; }

        IAddress Address { get; set; }
    }
}