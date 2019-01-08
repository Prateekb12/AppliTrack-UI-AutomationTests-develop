namespace ApplitrackUITests.DataModels
{
    public interface IUser : IPerson
    {
        long Id { get; set; }

        string UserName { get; set; }

        UserType Type { get; set; }
    }
}