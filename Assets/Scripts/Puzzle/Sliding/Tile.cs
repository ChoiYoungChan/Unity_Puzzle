using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{

    #region Private FIeld
    private TextMeshProUGUI _textNumber;
    private SlidingPuzzle _slidingPuzzleMap;
    private Vector3 _originalPos;
    private int tileNumber;
    #endregion

    #region Public Field
    public bool IsCorrect { get; private set; } = false;

    public int TileNumber
    {
        get => tileNumber;

        set
        {
            tileNumber = value;
            _textNumber.text = tileNumber.ToString();
        }
    }

    #endregion

    #region Private Method
    private IEnumerator MoveTile(Vector3 endPos)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        float moveTime = 0.1f;

        Vector3 startPos = GetComponent<RectTransform>().localPosition;

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / moveTime;

            GetComponent<RectTransform>().localPosition = Vector3.Lerp(startPos, endPos, percent);

            yield return null;
        }

        IsCorrect = _originalPos == GetComponent<RectTransform>().localPosition ? true : false;
        _slidingPuzzleMap.CheckClear();
    }

    #endregion

    #region Public Method
    public void InitializeTile(SlidingPuzzle map, int hideNumber, int number)
    {
        this._slidingPuzzleMap = map;
        _textNumber = GetComponentInChildren<TextMeshProUGUI>();
        TileNumber = number;

        // if it is a last tile, hide it because we need to move tile position by swipe
        if (TileNumber == hideNumber)
        {
            GetComponent<UnityEngine.UI.Image>().enabled = false;
            _textNumber.enabled = false;
        }
    }

    public void SetCorrectPosition()
    {
        _originalPos = this.GetComponent<RectTransform>().localPosition;
    }

    public void MoveTo(Vector3 destinationpos)
    {
        StartCoroutine("MoveTile", destinationpos);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _slidingPuzzleMap.MoveTile(this);
    }

    #endregion
}
