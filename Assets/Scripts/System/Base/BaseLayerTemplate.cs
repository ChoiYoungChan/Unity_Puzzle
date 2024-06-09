using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLayerTemplate : SingletonClass<BaseLayerTemplate>
{
    #region Inspector
    [Header("¡¼Layer Settings¡½")]
    [SerializeField] float _fadeTime = 0;
    #endregion

    #region Private Fields
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

    public virtual void MoveLayer()
    {

    }

    public virtual void OpenDialog(DialogManager.DialogKey _key)
    {

    }
    #endregion
}
