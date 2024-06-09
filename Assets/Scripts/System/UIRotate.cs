using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 1080f), 30f,RotateMode.FastBeyond360).SetLoops( -1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}