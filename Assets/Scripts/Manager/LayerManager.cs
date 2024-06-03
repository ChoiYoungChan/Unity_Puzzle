using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : SingletonClass<LayerManager>
{
    #region Enum
    public enum LayerKey
    {
        LayerKey_Top = 0,
        LayerKey_Select = 1,
        LayerKey_Play = 2,
        LayerKey_Result = 3,
        LayerKey_Max = 4
    }
    #endregion

    #region Inspector
    [SerializeField] BaseLayerTemplate[] _layerList;
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
        _fade.CrossFade();
        for (int count = 0; count < _layerList.Length; count++) {
            if((count == (int)key)) {
                _layerList[count].gameObject.SetActive(true);
            } else {
                _layerList[count].gameObject.SetActive(false);
            }
        }
    }

    public BaseLayerTemplate GetLayer(LayerKey key)
    {
        for (int count = 0; count < _layerList.Length; count++)
        {
            if ((count == (int)key)) return _layerList[count];
        }
        return null;
    }
    #endregion
}
