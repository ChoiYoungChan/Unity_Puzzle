using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidingPuzzle : BaseLayerTemplate
{
    #region Private Field
    [SerializeField] Transform _tileParent;
    [SerializeField] GameObject _tilePrefab;
    #endregion

    #region Private Field
    private List<Tile> _tileList = new List<Tile>();
    private Vector2Int _puzzleSize = new Vector2Int(6,6);
    private float nearTileDistance = 155;
    #endregion

    #region Public Field
    public Vector3 EmptyTilePosition { get; set; }
    public int PlayTime { get; private set; } = 0;
    public int MoveCount { get; private set; } = 0;
    #endregion

    #region Private Method
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        GenerateTile();

        // UI position will controll by GridLayoutGroup when sort through GridLayoutGroup
        // so, when tile is move, call this function to renew the tile position
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(_tileParent.GetComponent<RectTransform>());

        // stand by until nows frame is over
        yield return new WaitForEndOfFrame();

        // call SetCorrectPosition Method at every objects in List
        _tileList.ForEach(x => x.SetCorrectPosition());

        StartCoroutine("ShuffleTile");

        StartCoroutine("CalculatePlaytime");
    }

    private void GenerateTile()
    {
        for (int y_count = 0; y_count < _puzzleSize.y; ++ y_count)
        {
            for(int x_count = 0; x_count < _puzzleSize.x; ++x_count)
            {
                GameObject obj = Instantiate(_tilePrefab, _tileParent);
                Tile tile = obj.GetComponent<Tile>();
                tile.InitializeTile(this, (_puzzleSize.x * _puzzleSize.y), (y_count * _puzzleSize.x + x_count + 1));
                
                _tileList.Add(tile);
            }
        }
    }

    private IEnumerator ShuffleTile()
    {
        float currentTime = 0;
        float percent = 0;
        float time = 1.5f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / time;

            int randomIndex = Random.Range(0, (_puzzleSize.x * _puzzleSize.y));
            _tileList[randomIndex].transform.SetAsLastSibling();

            yield return null;
        }

        EmptyTilePosition = _tileList[_tileList.Count - 1].GetComponent<RectTransform>().localPosition;
    }

    private IEnumerator CalculatePlaytime()
    {
        while (true)
        {
            PlayTime++;
            yield return new WaitForSeconds(1);
        }
    }

    #endregion

    #region Public Method
    public override void Initialize()
    {
        base.Initialize();

    }

    public void MoveTile(Tile tile)
    {
        float distance = Vector3.Distance(EmptyTilePosition, tile.GetComponent<RectTransform>().localPosition);
        if (distance == nearTileDistance)
        {
            Vector3 targetPos = EmptyTilePosition;
            EmptyTilePosition = tile.GetComponent<RectTransform>().localPosition;
            tile.MoveTo(targetPos);
            
            MoveCount++;
        }
    }

    public void CheckClear()
    {
        List<Tile> tiles = _tileList.FindAll(x => x.IsCorrect == true);
        Debug.Log("Correct count : " + tiles.Count);

        if(tiles.Count == (_puzzleSize.x * _puzzleSize.y) - 1)
        {
            GameManager.Instance.IsClear = true;
            StopCoroutine("CalculatePlaytime");
            LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Result);
        }
    }
    #endregion
}
