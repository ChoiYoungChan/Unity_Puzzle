using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TopLayer : BaseLayerTemplate
{
    #region inspector
    [SerializeField] Button _startBtn;
    [SerializeField] Image _btnImage;
    #endregion

    #region Private Field
    Color _tempColor;
    #endregion

    #region Private Method
    void FlickSlowButton()
    {
        _btnImage.DOFade(0.0f, 1.5f).SetEase(Ease.InCubic).SetLoops(-1, LoopType.Yoyo);
    }
    void FlickFastButton()
    {
        SoundManager.Instance.Play("button_click");

        _btnImage.DOKill();
        _btnImage.color = _tempColor;
        _btnImage.DOFade(0.0f, 0.15f).SetEase(Ease.InCubic).SetLoops(-1, LoopType.Yoyo);

        DOTween.Sequence()
            .AppendInterval(1.0f)
            .AppendCallback(() => {
                _btnImage.DOKill();
            })
            .AppendInterval(0.05f)
            .AppendCallback(() => {
                MoveLayer();
                _btnImage.color = _tempColor;
            });
    }
    #endregion

    #region Public Method
    public override void Initialize()
    {
        SoundManager.Instance.Play("bgm");
        _startBtn.onClick.AddListener(FlickFastButton);
        _tempColor = _btnImage.color;
    }

    /// <summary>
    /// move to PlayLayer
    /// </summary>
    public override void MoveLayer()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Select);
    }
    #endregion
}
