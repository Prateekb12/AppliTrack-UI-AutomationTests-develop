namespace ApplitrackUITests.FeatureFlags
{
    public static class Term
    {
        private static string _interview = "Interview";

        /// <summary>
        /// Set the name for the "Interviews" in the system
        /// </summary>
        public static string Interview
        {
            get => _interview;
            set => _interview = value;
        }
    }
}
