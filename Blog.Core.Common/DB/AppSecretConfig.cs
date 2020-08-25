using System.IO;
using Blog.Core.Common.Helper;

namespace Blog.Core.Common.DB
{
    public class AppSecretConfig
    {
        private static string Audience_Secret = Appsettings.App(new string[] { "Audience", "Secret" });
        private static string Audience_Secret_File = Appsettings.App(new string[] { "Audience", "SecretFile" });

        public static string Audience_Secret_String => InitAudience_Secret();

        private static string InitAudience_Secret()
        {
            string securityString = DifDBConnOfSecurity(Audience_Secret_File);
            if(string.IsNullOrWhiteSpace(Audience_Secret_File) && string.IsNullOrWhiteSpace(securityString))
            {
                return securityString;
            }
            else
            {
                return Audience_Secret;
            }
        }

        private static string DifDBConnOfSecurity(params string[] conn)
        {
            foreach (var item in conn)
            {
                try
                {
                    if(File.Exists(item))
                    {
                        return File.ReadAllText(item);
                    }
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return string.Empty;
        }
    }
}