namespace jiraF.Goal.API.GlobalVariables
{
    /// <summary>
    /// Variables for default member.
    /// </summary>
    public static class DefaultMemberVariables
    {
        private static string _id;

        /// <summary>
        /// Read-only member Id.
        /// </summary>
        public static string Id
        {
            get => _id;
            set
            {
                if (_id == null)
                {
                    _id = value;
                }
            }
        }
    }
}
