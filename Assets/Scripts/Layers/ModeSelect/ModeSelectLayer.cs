using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelectLayer : BaseLayer
{
    #region Inspector

    [SerializeField] private Button _backBtn;
    [SerializeField] private Button _slidingBtn;
    [SerializeField] private Button _jigsawBtn;
    [SerializeField] private Button _rankingBtn;
    #endregion

    #region Private Method
    private void Start()
    {
        
    }

    private void OnClickBackButton()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.Top);
    }

    private void OnClickStageButton(PlayLayer.PuzzleType type)
    {
        GameObject layerobj = LayerManager.Instance.GetLayer(LayerManager.LayerKey.Play).gameObject;
        layerobj.GetComponent<PlayLayer>().Type = type;

        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.Play);
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
        _rankingBtn?.onClick.AddListener(() => { OpenDialog(DialogManager.DialogKey.Ranking); });
    }

    public override void OpenDialog(DialogManager.DialogKey _key)
    {
        DialogManager.Instance.OpenDialog(_key);
    }

    #endregion
}
