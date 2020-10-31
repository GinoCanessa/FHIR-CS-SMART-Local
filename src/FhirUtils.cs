using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;

namespace smart_local
{
    /// <summary>
    /// FHIR Utility functions
    /// </summary>
    public static class FhirUtils
    {
      public static bool TryGetSmartUrls(
        FhirClient fhirClient,
        out string authorizeUrl,
        out string tokenUrl
      )
      {
        authorizeUrl = string.Empty;
        tokenUrl = string.Empty;

        CapabilityStatement capabilities = (CapabilityStatement)fhirClient.Get("metadata");

        foreach (CapabilityStatement.RestComponent restComponent in capabilities.Rest)
        {
          if (restComponent.Security == null)
          {
            continue;
          }

          foreach (Extension securityExt in restComponent.Security.Extension)
          {
            if (securityExt.Url != "http://fhir-registry.smarthealthit.org/StructureDefinition/oauth-uris")
            {
              continue;
            }

            if ((securityExt.Extension == null) || (securityExt.Extension.Count == 0))
            {
              continue;
            }

            foreach (Extension smartExt in securityExt.Extension)
            {
              switch (smartExt.Url)
              {
                case "authorize":
                  authorizeUrl = ((FhirUri)smartExt.Value).Value.ToString();
                  break;

                case "token":
                  tokenUrl = ((FhirUri)smartExt.Value).Value.ToString();
                  break;
              }
            }
          }
        }

        if (string.IsNullOrEmpty(authorizeUrl) || string.IsNullOrEmpty(tokenUrl))
        {
          return false;
        }

        return true;
      }

    }
}
