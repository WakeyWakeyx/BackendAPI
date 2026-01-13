using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WakeyWakeyBackendAPI.Models.Converters
{
    internal class EncryptedStringConverter : ValueConverter<string, string>
    {
        internal EncryptedStringConverter(IDataProtectionProvider protectionProvider) : base
        (
            model => protectionProvider.CreateProtector("Sensitive").Protect(model),
            provider => protectionProvider.CreateProtector("Sensitive").Unprotect(provider)
        ) {}
    }
}
