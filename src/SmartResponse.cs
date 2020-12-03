using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace smart_local
{
  /// <summary>
  /// Class to deserialize SMART auth responses.
  /// </summary>
  public class SmartResponse
  {
    [JsonPropertyName("need_patient_banner")]
    public bool NeedPatientBanner { get; set; }

    [JsonPropertyName("smart_style_url")]
    public string SmartStyleUrl { get; set; }

    [JsonPropertyName("patient")]
    public string PatientId { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("scope")]
    public string Scopes { get; set; }

    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresInSeconds { get; set; }

    [JsonPropertyName("id_token")]
    public string IdToken { get; set; }

    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
  }
}