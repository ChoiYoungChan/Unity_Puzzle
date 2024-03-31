using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : SingletonClass<LayerManager>
{
    [SerializeField] GameObject[] _layerList;

    public enum LayerKey {
        LayerKey_Top = 0,
        LayerKey_Select = 1,
        LayerKey_Play = 2,
        LayerKey_Result = 3,
        LayerKey_Max = 4
    }

    public void MoveLayer(LayerKey key)
    {
        for(int count = 0; count < _layerList.Length; count++) {
            if((count == (int)key)) {
                _layerList[count].SetActive(true);
            } else {
                _layerList[count].SetActive(false);
            }
        }
    }

    public GameObject GetLayer(LayerKey key)
    {
        for (int count = 0; count < _layerList.Length; count++)
        {
            if ((count == (int)key)) return _layerList[count];
        }
        return null;
    }
}
