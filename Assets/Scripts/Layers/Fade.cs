using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : BaseLayerTemplate
{
    #region PrivateFields

    [SerializeField] Image _bgImage;

    #endregion

    #region Public Method

    public void CrossFade()
    {
        transform.SetAsLastSibling();
        _bgImage.enabled = true;
        _bgImage.raycastTarget = true;
        DOTween.Sequence()
            .Append(_bgImage.DOFade(1.0f, LayerManager.Instance.FadeTime))
            .AppendInterval(1.0f)
            .Append(_bgImage.DOFade(0, LayerManager.Instance.FadeTime))
            .AppendCallback(() => { _bgImage.raycastTarget = false; });
    }
    #endregion
}
