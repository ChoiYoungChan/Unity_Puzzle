using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RemoteData
{
    [JsonProperty("data")]
    public StageData[] Data { get; set; }

    [JsonProperty("userdata")]
    public UserData UserData { get; set; }
}
