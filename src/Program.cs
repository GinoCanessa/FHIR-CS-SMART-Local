using System;

namespace smart_local
{
    /// <summary>
    /// Main program
    /// </summary>
    public static class Program
    {
        private const string _defaultFhirServerUrl = "https://launch.smarthealthit.org/v/r4/sim/eyJoIjoiMSIsImUiOiJlZmI1ZDRjZS1kZmZjLTQ3ZGYtYWE2ZC0wNWQzNzJmZGI0MDcifQ/fhir/";

        /// <summary>
        /// Program to access a SMART FHIR Server with a local webserver for redirection
        /// </summary>
        /// <param name="fhirServerUrl">FHIR R4 endpoint URL</param>
        /// <returns></returns>
        static int Main(
            string fhirServerUrl
        )
        {
            if (string.IsNullOrEmpty(fhirServerUrl))
            {
                fhirServerUrl = _defaultFhirServerUrl;
            }

            System.Console.WriteLine($"  FHIR Server: {fhirServerUrl}");

            Hl7.Fhir.Rest.FhirClient fhirClient = new Hl7.Fhir.Rest.FhirClient(fhirServerUrl);

            if (!FhirUtils.TryGetSmartUrls(fhirClient, out string authorizeUrl, out string tokenUrl))
            {
                System.Console.WriteLine($"Failed to discover SMART URLs");
                return -1;
            }

            System.Console.WriteLine($"Authorize URL: {authorizeUrl}");
            System.Console.WriteLine($"    Token URL: {tokenUrl}");

            return 0;
        }
    }
}
