using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDialog : MonoBehaviour
{
    public virtual void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public virtual void Initialize()
    {

    }

    public void Show()
    {
        DOTween.Sequence()
            .Append(transform.DOLocalMoveY(0, 0.5f));
    }

    public void Hide()
    {
        DOTween.Sequence()
            .Append(transform.DOLocalMoveY(-5000, 0.5f));
    }
}
