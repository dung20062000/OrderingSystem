using OrderingSystem.Debugging;

namespace OrderingSystem
{
    public class OrderingSystemConsts
    {
        public const string LocalizationSourceName = "OrderingSystem";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "da4ce7988b874b398d2253190c6506c4";
    }
}
