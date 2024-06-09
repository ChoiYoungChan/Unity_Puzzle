using Newtonsoft.Json;
using UnityEngine;

public class UserData
{
    [JsonProperty("UserId")]
    public int UserId { get; set; }

    [JsonProperty("UserName")]
    public string UserName { get; set; }

    [JsonProperty("Password")]
    public string Password { get; set; }

    [JsonProperty("Score")]
    public string Score { get; set; }

    [JsonProperty("ClearedStage")]
    public string ClearedStage { get; set; }
}
