using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JigsawPuzzle : BaseLayerTemplate
{
    #region Inspector
    [Header("UI")]
    [SerializeField] private List<Sprite> _puzzleImage;
    [SerializeField] private Transform _mapSize;
    [Space(2)]

    [Header("Parameter")]
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

    #region Public Field
    public int Difficulty { get; set; } = 4;

    #endregion


    #region Private Method

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit)
            {
                _draggingPiece = hit.transform;
                _offset = _draggingPiece.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _offset += Vector3.back;
            }
        }

        // When we release the mouse button stop dragging.
        if (_draggingPiece && Input.GetMouseButtonUp(0))
        {
            SnapAndClearCheck();
            _draggingPiece.position += Vector3.forward;
            _draggingPiece = null;
        }

        // Set the dragged piece position to the position of the mouse.
        if (_draggingPiece)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //newPosition.z = draggingPiece.position.z;
            newPosition += _offset;
            _draggingPiece.position = newPosition;
        }
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
                piece.localPosition = new Vector2(
                  (-_width * _puzzlesize.x / 2) + (_width * x_count) + (_width / 2),
                  (-_height * _puzzlesize.y / 2) + (_height * y_count) + (_height / 2));

                piece.localScale = new Vector2(_width, _height);

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
        // Calculate the visible orthographic size of the screen.
        float orthoHeight = Camera.main.orthographicSize;
        float screenAspect = (float)Screen.width / Screen.height;
        float orthoWidth = (screenAspect * orthoHeight);

        // Ensure pieces are away from the edges.
        float pieceWidth = _width * _gameHolder.localScale.x;
        float pieceHeight = _height * _gameHolder.localScale.y;

        orthoHeight -= pieceHeight;
        orthoWidth -= pieceWidth;

        // Place each piece randomly in the visible area.
        foreach (Transform piece in _pieces)
        {
            float xAxis = Random.Range(-orthoWidth, orthoWidth);
            float yAxis = Random.Range(-orthoHeight, orthoHeight);
            piece.position = new Vector2(xAxis, yAxis);
        }
    }

    private void SnapAndClearCheck()
    {
        // We need to know the index of the piece to determine it's correct position.
        int pieceIndex = _pieces.IndexOf(_draggingPiece);

        // The coordinates of the piece in the puzzle.
        int col = pieceIndex % _puzzlesize.x;
        int row = pieceIndex / _puzzlesize.x;

        // The target position in the non-scaled coordinates.
        Vector2 targetPosition = new((-_width * _puzzlesize.x / 2) + (_width * col) + (_width / 2),
                                     (-_height * _puzzlesize.y / 2) + (_height * row) + (_height / 2));

        // Check if we're in the correct location.
        if (Vector2.Distance(_draggingPiece.localPosition, targetPosition) < (_width / 2))
        {
            // Snap to our destination.
            _draggingPiece.localPosition = targetPosition;

            // Disable the collider so we can't click on the object anymore.
            _draggingPiece.GetComponent<BoxCollider2D>().enabled = false;

            // Increase the number of correct pieces, and check for puzzle completion.
            _correctNumber++;
            if (_correctNumber == _pieces.Count)
            {
                LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Result);
            }
        }
    }

    private void UpdateBorder()
    {
        LineRenderer lineRenderer = _gameHolder.GetComponent<LineRenderer>();

        // Calculate half sizes to simplify the code.
        float halfWidth = (_width * _puzzlesize.x) / 2f;
        float halfHeight = (_height * _puzzlesize.y) / 2f;

        // We want the border to be behind the pieces.
        float borderZ = 0f;

        // Set border vertices, starting top left, going clockwise.
        lineRenderer.SetPosition(0, new Vector3(-halfWidth, halfHeight, borderZ));
        lineRenderer.SetPosition(1, new Vector3(halfWidth, halfHeight, borderZ));
        lineRenderer.SetPosition(2, new Vector3(halfWidth, -halfHeight, borderZ));
        lineRenderer.SetPosition(3, new Vector3(-halfWidth, -halfHeight, borderZ));

        // Set the thickness of the border line.
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // Show the border line.
        lineRenderer.enabled = true;
    }

    private Vector2Int GetSize(Texture2D texture)
    {
        Vector2Int dimensions = Vector2Int.zero;
        // Difficulty is the number of pieces on the smallest texture dimension.
        // This helps ensure the pieces are as square as possible.
        if (texture.width < texture.height) {
            dimensions.x = Difficulty;
            dimensions.y = (Difficulty * texture.height) / texture.width;
        } else {
            dimensions.x = (Difficulty * texture.width) / texture.height;
            dimensions.y = Difficulty;
        }
        return dimensions;
    }

    #endregion


    #region Public Method
    public override void Initialize()
    {
        base.Initialize();
        _pieces = new List<Transform>();
        _puzzlesize = GetSize(_puzzleImage[0].texture);

        SplitJigsawPieces(_puzzleImage[0].texture);
        SpreadPieces();
        //UpdateBorder();

        _correctNumber = 0;
    }

    #endregion

}
