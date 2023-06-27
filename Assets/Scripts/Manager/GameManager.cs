using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    public static bool IsSoundOn, IsClear;
    public static int HintCount;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        InitializeData();
    }

    private void InitializeData()
    {
        IsSoundOn = true;
        IsClear = false;
        HintCount = 0;
        SetSoundOn(IsSoundOn);
    }

    private void Start()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Top);
    }

    public void SetSoundOn(bool _isActive)
    {
        IsSoundOn = _isActive;
        CacheData.Instance.SoundOn = IsSoundOn;
    }

    public bool GetSoundOn()
    {
        return IsSoundOn;
    }

    public void SetIsClear(bool _isActive)
    {
        IsClear = _isActive;
    }

    public bool GetIsClear()
    {
        return IsClear;
    }

    public void SetHintCount(int _count)
    {
        HintCount = _count;
        CacheData.Instance.HintCount = HintCount;
    }

    public int GetHintCount()
    {
        return HintCount;
    }
}
