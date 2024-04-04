using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JigsawPuzzle : BaseLayerTemplate
{
    #region Inspector
    [Header("UI")]
    [SerializeField] private Image _stageImage;
    [SerializeField] private List<Sprite> _puzzleImage;
    [SerializeField] private Transform _mapSize;
    [SerializeField] private Image levelSelectPrefab;
    [SerializeField] private GameObject _playAgainButton;
    [Space(2)]

    [Header("Parameter")]
    [Range(2, 6)]
    [SerializeField] private Transform _gameHolder;
    [SerializeField] private Transform _piecePrefab;
    #endregion

    #region Private FIeld
    private List<Transform> _pieces;
    private Vector2Int _puzzlesize;
    private float _width;
    private float _height;

    private Transform _draggingPiece = null;
    private Vector3 _offset;

    private int _correctNumber;
    #endregion


    #region Private Method
    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void SplitJigsawPieces(Texture2D texture)
    {
        float aspect = (float)texture.width / texture.height;
        _width = aspect / _puzzlesize.x;
        _height = 1.0f / _puzzlesize.y;

        for (int y_count = 0; y_count < _puzzlesize.y; y_count++)
        {
            for (int x_count = 0; x_count < _puzzlesize.x; x_count++)
            {
                // Create the piece in the right location of the right size.
                Transform piece = Instantiate(_piecePrefab, _gameHolder);
                piece.localPosition = new Vector3(
                  (-_width * _puzzlesize.x / 2) + (_width * x_count) + (_width / 2),
                  (-_height * _puzzlesize.y / 2) + (_height * y_count) + (_height / 2),
                  -1);
                piece.localScale = new Vector3(_width, _height, 1f);

                // We don't have to name them, but always useful for debugging.
                piece.name = $"Piece {(y_count * _puzzlesize.x) + x_count}";
                _pieces.Add(piece);

                // Assign the correct part of the texture for this jigsaw piece
                // We need our width and height both to be normalised between 0 and 1 for the UV.
                float width1 = 1f / _puzzlesize.x;
                float height1 = 1f / _puzzlesize.y;
                // UV coord order is anti-clockwise: (0, 0), (1, 0), (0, 1), (1, 1)
                Vector2[] uv = new Vector2[4];
                uv[0] = new Vector2(width1 * x_count, height1 * y_count);
                uv[1] = new Vector2(width1 * (x_count + 1), height1 * y_count);
                uv[2] = new Vector2(width1 * x_count, height1 * (y_count + 1));
                uv[3] = new Vector2(width1 * (x_count + 1), height1 * (y_count + 1));
                // Assign our new UVs to the mesh.
                Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                mesh.uv = uv;
                // Update the texture on the piece
                piece.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
            }
        }
    }

    private void SpreadPieces()
    {

    }

    private void CheckClear()
    {

    }

    Vector2Int GetSize(Texture2D texture)
    {
        int difficulty = 4;
        Vector2Int dimensions = Vector2Int.zero;
        // Difficulty is the number of pieces on the smallest texture dimension.
        // This helps ensure the pieces are as square as possible.
        if (texture.width < texture.height) {
            dimensions.x = difficulty;
            dimensions.y = (difficulty * texture.height) / texture.width;
        } else {
            dimensions.x = (difficulty * texture.width) / texture.height;
            dimensions.y = difficulty;
        }
        return dimensions;
    }

    #endregion


    #region Public Method
    public override void Initialize()
    {
        base.Initialize();
        _stageImage.sprite = _puzzleImage[0];
        //_stageImage.sprite = Resources.Load<Sprite>("illust/stage/" + .Replace(".jpg", "").Replace(".png", ""));
        //_puzzlesize = GetSize();
    }


    #endregion

}
