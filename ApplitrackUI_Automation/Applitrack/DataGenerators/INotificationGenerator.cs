namespace ApplitrackUITests.DataGenerators
{
    public interface INotificationGenerator
    {
        /// <summary>
        /// Contains a representation of the expected data contained in the resulting notification
        /// </summary>
        NotificationResult ExpectedResult { get; set; }

        /// <summary>
        /// Create all requisite database data needed to drive the implemented type of notification
        /// </summary>
        void CreateNotificationData();

        /// <summary>
        /// Remove the data used by the test
        /// </summary>
        void DeleteNotificationData();
    }
}
