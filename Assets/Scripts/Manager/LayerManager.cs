using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : SingletonClass<LayerManager>
{
    #region Enum
    public enum LayerKey
    {
        Loading = 0,
        Top = 1,
        Select = 2,
        Play = 3,
        Result = 4,
        Max = 5
    }
    #endregion

    #region Inspector
    [SerializeField] BaseLayer[] _layerList;
    [SerializeField] Fade _fade;
    [SerializeField] Canvas _targetCanvas;
    #endregion

    #region Public FIeld
    public Fade Fade { get { return _fade; } set { _fade = value; } }

    public float FadeTime { get; set; }

    public Canvas TargetCanvas
    {
        get { return _targetCanvas; }
    }

    #endregion


    #region Public Method
    public void MoveLayer(LayerKey key)
    {
        for (int count = 0; count < _layerList.Length; count++) {
            if (count.Equals((int)key)) {
                _layerList[count].gameObject.SetActive(true);
                var layer = _layerList[count] as BaseLayer;
                layer.Initialize();
            } else {
                _layerList[count].gameObject.SetActive(false);
            }
        }
    }

    public BaseLayer GetLayer(LayerKey key)
    {
        for (int count = 0; count < _layerList.Length; count++)
        {
            if ((count == (int)key)) return _layerList[count];
        }
        return null;
    }
    #endregion
}
