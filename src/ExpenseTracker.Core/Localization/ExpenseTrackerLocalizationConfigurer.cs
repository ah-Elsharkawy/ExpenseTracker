using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace ExpenseTracker.Localization
{
    public static class ExpenseTrackerLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(ExpenseTrackerConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ExpenseTrackerLocalizationConfigurer).GetAssembly(),
                        "ExpenseTracker.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
