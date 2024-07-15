using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    bool isTouch = false;
    bool isCollision = false;
    bool useCollision = false;

    void Start()
    {

    }

    // パズルを生成する.
    public void Setup(Texture2D origin, Vector2 trimPosition, Vector2 trimSize)
    {
        Triming trim = new Triming();
        Image img = GetComponent<Image>();
        img.sprite = trim.GetTriming(origin, trimPosition, trimSize);
        img.SetNativeSize();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = trimSize;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -450.0f);
    }


    public void Setup(Texture2D origin, Vector2 trimPosition, Quaternion trimrotate, Vector2 trimSize)
    {
        Triming trim = new Triming();
        Image img = GetComponent<Image>();
        img.sprite = trim.GetTriming(origin, trimPosition, trimSize);
        img.SetNativeSize();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = trimSize;
        GetComponent<RectTransform>().rotation = trimrotate;
    }

    public void OnPointerUp()
    {
        isTouch = false;
        if (isCollision) useCollision = true;
        else useCollision = false;

        //GetComponent<RectTransform>().rotation.z += 90;
        GetComponent<RectTransform>().rotation *= Quaternion.Euler(0, 0, 90);
        if (GetComponent<RectTransform>().rotation == Quaternion.Euler(0, 0, 0))
        {
            RotatePuzzle play = LayerManager.Instance.GetLayer(LayerManager.LayerKey.Play) as RotatePuzzle;
            play.CheckAnswer();
        }
    }

    public void SetItem()
    {
        gameObject.SetActive(true);
    }
}
