using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopLayer : BaseLayerTemplate
{
    [SerializeField] Button _startBtn, _soundBtn, _reviewBtn;

    /// <summary>
    /// Initialize
    /// </summary>
    public virtual void Initialize()
    {
        _startBtn.onClick.AddListener(OnClickStartButton);
        _soundBtn.onClick.AddListener(OnClickSoundButton);
        _reviewBtn.onClick.AddListener(OnClickReviewButton);
    }

    private void OnClickStartButton()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Play);
    }

    private void OnClickSoundButton()
    {
        if (GameManager.Instance.GetSoundOn()) {
            GameManager.Instance.SetSoundOn(false);
        } else {
            GameManager.Instance.SetSoundOn(true);
        }
    }

    private void OnClickReviewButton()
    {

    }
}
