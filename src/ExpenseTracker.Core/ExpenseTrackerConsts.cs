using ExpenseTracker.Debugging;

namespace ExpenseTracker
{
    public class ExpenseTrackerConsts
    {
        public const string LocalizationSourceName = "ExpenseTracker";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "1a3e4d3f627047e7b30ed459345605a1";
    }
}
