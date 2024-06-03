using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triming
{
    public Sprite GetTriming(Texture2D texture, Vector2 postion, Vector2 size)
    {
        return Sprite.Create(texture, new Rect(postion.x, postion.y, size.x, size.y), Vector2.zero);
    }

    public Sprite GetTrimingSquare(Texture2D texture, Vector2 postion, float size)
    {
        return Sprite.Create(texture, new Rect(postion.x, postion.y, size, size), Vector2.zero);
    }
}
