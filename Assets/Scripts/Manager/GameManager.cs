using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    #region Properties
    public bool IsSoundOn { get; set; }
    public bool IsClear { get; set; }
    public int PuzzleMode { get; set; }
    public bool isPlaying { get; set; }
    public int SelectStageNumber { get; set; }
    public bool IsRetry { get; set; }
    public bool IsNext { get; set; }

    public StageData[] PlayData;

    #endregion

    #region Private Method
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        InitializeData();
    }

    private void InitializeData()
    {
        IsSoundOn = true;
        IsClear = false;
        CacheData.Instance.SoundOn = IsSoundOn;
    }

    private void Start()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Top);
    }
    #endregion

    #region Public Method

    public void GetGameData()
    {
        var data = RemoteDataManager.Instance.GetData();
        PlayData = data;
    }
    #endregion
}
