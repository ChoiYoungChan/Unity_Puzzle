using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayLayer : BaseLayerTemplate
{
    [SerializeField] Button _backBtn, _settingBtn;
    [SerializeField] GameObject[] _puzzleObj;
    private int DragCount;
    private bool _isClear;

    /// <summary>
    /// Initialize
    /// </summary>
    public override void Initialize()
    {
        _backBtn.onClick.AddListener(OnClickBackButton);
        _settingBtn.onClick.AddListener(() => { DialogManager.Instance.OpenDialog(DialogManager.DialogKey.DialogKey_Setting); });
    }

    private void Start()
    {
        DragCount = 0;

    }

    private void OnClickBackButton()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Top);
    }

    private void MoveResultLayer()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Result);
    }

    private void InitializePuzzle()
    {

    }

    private void UpdatePuzzleBlock()
    {

    }

    private void MovePuzzleBlock()
    {

    }

    /// <summary>
    /// check result like game clear or game fail
    /// </summary>
    private void CheckResult()
    {
        GameManager.Instance.SetIsClear(_isClear);
        MoveResultLayer();
    }
}
