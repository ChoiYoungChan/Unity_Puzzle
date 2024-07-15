using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.IO;
using Newtonsoft.Json;

public class RemoteDataManager : SingletonClass<RemoteDataManager>
{
    public static RemoteData remoteData;
    public static bool _done = false;
    public static bool _saveDone = false;


    private string _remoteUrl = "";
    private const string MsgDataFileName = "MsgData.bin";

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        //StartCoroutine(Download(_remoteUrl));
        // TODO: set current url in inspector
    }

    void Start()
    {

    }

    public void RetryDownload()
    {
        //StartCoroutine(Download(_remoteUrl));
    }

    public RemoteData GetRemoteData()
    {
        return remoteData;
    }

    public StageData[] GetData()
    {
        var ret = remoteData.Data;
#if UNITY_ANDROID
            ret = remoteData.Data_a;
        if (ret == null) ret = remoteData.Data;
#endif
        return ret;
    }

    public IEnumerator Download(string sURL)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(sURL))
        {
            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.LogError(www.error);
                Debug.Log("### Data Load Fail");
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                SaveMsgData(System.Text.Encoding.UTF8.GetString(results));
                _done = true;
                Debug.Log("### Data Load Success");
            }
        }
    }

    public void SaveMsgData(string json)
    {
        // parse json
        if (json != null)
        {
            remoteData = JsonConvert.DeserializeObject<RemoteData>(json);

            return;
        }
        string path = StreamingAssetPath();
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
        json = json.Replace("<BR>", Environment.NewLine);
        File.WriteAllText(FilePath(MsgDataFileName), json);

#if UNITY_IOS
        UnityEngine.iOS.Device.SetNoBackupFlag(Application.persistentDataPath);
#endif
    }

    public string FilePath(string file_name)
    {
        string path = StreamingAssetPath();
        return string.Format("{0}/{1}", path, file_name);
    }

    public string StreamingAssetPath()
    {
        var dataPath = Application.streamingAssetsPath;
#if UNITY_EDITOR
        dataPath = Application.dataPath;
#elif UNITY_IOS
            dataPath = Application.persistentDataPath;
#elif UNITY_ANDROID
            dataPath = Application.persistentDataPath;
#else
            dataPath = Application.streamingAssetsPath;
#endif

#if UNITY_EDITOR
        string path = dataPath;
        path = path.Substring(0, path.LastIndexOf('/'));
        path += "/res";
        return path;
#else
        return dataPath + "/res";
#endif
    }
}
