using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelectLayer : BaseLayerTemplate
{
    #region Inspector

    [SerializeField] private Button _backBtn;
    [SerializeField] private Button _slidingBtn;
    [SerializeField] private Button _jigsawBtn;
    #endregion

    #region Private Method
    private void Start()
    {
        
    }

    private void OnClickBackButton()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Top);
    }

    private void OnClickStageButton(PlayLayer.PuzzleType type)
    {
        GameObject layerobj = LayerManager.Instance.GetLayer(LayerManager.LayerKey.LayerKey_Play);
        layerobj.GetComponent<PlayLayer>().Type = type;

        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Play);
    }

    #endregion

    #region Public Method

    /// <summary>
    /// Initialize
    /// </summary>
    public override void Initialize()
    {
        _backBtn?.onClick.AddListener(OnClickBackButton);
        _slidingBtn?.onClick.AddListener(() => { OnClickStageButton(PlayLayer.PuzzleType.Sliding); });
        _jigsawBtn?.onClick.AddListener(() => { OnClickStageButton(PlayLayer.PuzzleType.JigSaw); });
    }

    #endregion
}
