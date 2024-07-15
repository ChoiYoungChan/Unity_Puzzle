using Newtonsoft.Json;
using UnityEngine;

public class UserData
{
    [JsonProperty("UserId")]
    public string UserId { get; set; }

    [JsonProperty("Password")]
    public string Password { get; set; }

    [JsonProperty("Score")]
    public string Score { get; set; }
}
