using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopLayer : BaseLayerTemplate
{
    [SerializeField] Button _startBtn;

    /// <summary>
    /// Initialize
    /// </summary>
    public override void Initialize()
    {
        _startBtn.onClick.AddListener(MoveLayer);
    }

    /// <summary>
    /// move to PlayLayer
    /// </summary>
    public override void MoveLayer()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Select);
    }
}
