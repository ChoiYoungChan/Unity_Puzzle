using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class NetWorkManager : SingletonClass<NetWorkManager>
{
    private string _pwd = "";
    // Start is called before the first frame update

    private string _loginUrl = "192.168.1.12:8080/login.php";
    private string _registerUrl = "192.168.1.12:8080/register.php";
    private string _setScoreUrl = "192.168.1.12:8080/set_score.php";
    private string _getUserDataUrl = "192.168.1.12:8080/get_user_data.php";

    public async UniTask LogIn(string id, string pass)
    {
        await NetworkingAsync(_loginUrl, id, pass);
    }

    public async UniTask MakeAccount(string id, string pass)
    {
        await NetworkingAsync(_registerUrl, id, pass);
    }

    public async UniTask SetUserData(UserData userdata)
    {
        await SetUserDataAsync(userdata);
    }

    public async UniTask GetUserData()
    {
        await GetUserDataAsync(CacheData.Instance.ID);
    }

    private async UniTask NetworkingAsync(string url, string id, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("pass", pass);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            var operation = await www.SendWebRequest().ToUniTask();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Failed: " + www.error);
            }
            else
            {
                Debug.Log("### Response: " + www.downloadHandler.text);

                if (www.downloadHandler.text == "success")
                {
                    CacheData.Instance.ID = id;
                    _pwd = pass;
                    Debug.Log("Successful");
                } else Debug.Log("## Failed: " + www.downloadHandler.text);
            }
        }
    }

    private async UniTask SetUserDataAsync(UserData userdata)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", userdata.UserId);
        form.AddField("score", userdata.Score);

        using (UnityWebRequest www = UnityWebRequest.Post(_setScoreUrl, form))
        {
            var operation = await www.SendWebRequest().ToUniTask();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("### SetUserData failed: " + www.error);
            }
            else
            {
                Debug.Log("### SetUserData response: " + www.downloadHandler.text);
                if (www.downloadHandler.text == "success") Debug.Log("### SetUserData successful");
                else Debug.Log("## SetUserData failed: " + www.downloadHandler.text);
            }
        }
    }

    private async UniTask GetUserDataAsync(string userId)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", userId);

        using (UnityWebRequest www = UnityWebRequest.Post(_getUserDataUrl, form))
        {
            var operation = await www.SendWebRequest().ToUniTask();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("### GetUserData failed: " + www.error);
            }
            else
            {
                Debug.Log("### GetUserData response: " + www.downloadHandler.text);
                if (www.downloadHandler.text != "failure") {
                    UserData userdata = JsonConvert.DeserializeObject<UserData>(www.downloadHandler.text);
                    Debug.Log("### GetUserData successful");
                } else {
                    Debug.Log("## GetUserData failed: User not found");
                }
            }
        }
    }

    public async UniTaskVoid GetGameDataAsync()
    {
        var cts = new CancellationTokenSource();
        //await GetRemoteData(cts);
    }
    public async UniTask<RemoteData> GetRemoteData(CancellationToken cancelToken)
    {
        var result = await Request("", true, cancelToken);
        result ??= await Request("", false, cancelToken);
        var jsonResult = JsonConvert.DeserializeObject<RemoteData>(result);
        return jsonResult;
    }

    public async UniTask<string> Request(string url, bool breakthrough, CancellationToken cancelToken)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            try {
                await request.SendWebRequest().WithCancellation(cancelToken);
            } catch {
                if (breakthrough) return null;
            }

            if (request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.DataProcessingError)
            {
                throw new Exception(request.error);
            }

            byte[] results = request.downloadHandler.data;
            return System.Text.Encoding.UTF8.GetString(results);
        }
    }
}
