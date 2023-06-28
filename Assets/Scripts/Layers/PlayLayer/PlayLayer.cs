using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayLayer : BaseLayerTemplate
{
    [SerializeField] Button backBtn, configBtn;
    private int DragCount;

    public override void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public override void Initialize()
    {
        backBtn.onClick.AddListener(OnClickBackButton);
        configBtn.onClick.AddListener(() => { DialogManager.Instance.OpenDialog(DialogManager.DialogKey.DialogKey_Setting); });
    }

    /// <summary>
    /// move back to TopLayer
    /// </summary>
    public override void MoveLayer()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Top);
    }

    private void Start()
    {
        DragCount = 0;

    }

    private void OnClickBackButton()
    {
        MoveLayer();
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

    // set game score
    private void SetScore()
    {

    }

    /// <summary>
    /// check result like game clear or game fail
    /// </summary>
    private void CheckResult()
    {

    }
}
