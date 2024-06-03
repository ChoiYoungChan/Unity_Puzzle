using UnityEngine;

public class TouchManager
{
    public bool _touchFlag;
    public Vector2 _touchPosition;
    public TouchPhase _touchPhase;

    public TouchManager(bool flag = false, Vector2? position = null, TouchPhase phase = TouchPhase.Began)
    {
        this._touchFlag = flag;
        if (position == null)
        {
            this._touchPosition = new Vector2(0, 0);
        }
        else
        {
            this._touchPosition = (Vector2)position;
        }
        this._touchPhase = phase;
    }

    public void update()
    {
        this._touchFlag = false;

        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this._touchFlag = true;
                this._touchPhase = TouchPhase.Began;
                //Debug.Log("### 押した瞬間");
            }

            // 離した瞬間
            else if (Input.GetMouseButtonUp(0))
            {
                this._touchFlag = true;
                this._touchPhase = TouchPhase.Ended;
                //Debug.Log("### 離した瞬間");
            }

            // 押しっぱなし
            else if (Input.GetMouseButton(0))
            {
                this._touchFlag = true;
                this._touchPhase = TouchPhase.Moved;
                //Debug.Log("### 押しっぱなし");
            }

            if (this._touchFlag) this._touchPosition = Input.mousePosition;
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                this._touchPosition = touch.position;
                this._touchPhase = touch.phase;
                this._touchFlag = true;
            }
        }
    }

    public TouchManager getTouch()
    {
        return new TouchManager(this._touchFlag, this._touchPosition, this._touchPhase);
    }
}


