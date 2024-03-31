using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    #region Properties
    public bool IsSoundOn { get; set; }
    public bool IsClear { get; set; }
    public int PuzzleMode { get; set; }
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
}
