using System.Collections.Generic;

namespace itec_backend.Helpers
{

    public static class CountryTranslator
    {
        static Dictionary<string, string> countries = new Dictionary<string, string>
        {
            {"Deutschland", "Germany"},
            {"România", "Romania"},
            {"Italia", "Italy"},
            {"Magyarország", "Hungary"},
            {"Österreich", "Austria"},
            {"España", "Spain"},
        };

        public static string Translate(string s)
        {

            foreach (KeyValuePair<string, string> entry in countries)
            {
                if (entry.Key.ToLower() == s.ToLower())
                    return entry.Value;
            }

            return s;
        }
    }
}