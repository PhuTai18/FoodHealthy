using ITHealthy.Models;

namespace ITHealthy.Helpers
{
    public static class HelperAddress
    {
        public static string ToFullAddress(this Store store)
        {
            return string.Join(", ", new[]
            {
                store.StreetAddress,
                store.Ward,
                store.District,
                store.City,
                store.Country
            }.Where(x => !string.IsNullOrEmpty(x)));
        }
    }
}