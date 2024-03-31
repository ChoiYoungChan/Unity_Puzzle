using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JigsawPuzzle : BaseLayerTemplate
{
    #region Inspector
    [Header("UI")]
    [SerializeField] private List<Texture2D> imageTextures;
    [SerializeField] private Transform levelSelectPanel;
    [SerializeField] private Image levelSelectPrefab;
    [SerializeField] private GameObject playAgainButton;
    [Space(2)]

    [Header("Parameter")]
    [Range(2, 6)]
    [SerializeField] private Transform gameHolder;
    [SerializeField] private Transform piecePrefab;
    #endregion

    #region Private FIeld
    private List<Transform> _pieces;
    private Vector2Int _dimensions;
    private float _width;
    private float _height;

    private Transform _draggingPiece = null;
    private Vector3 _offset;

    private int _correctNumber;
    private int _difficulty = 4;
    #endregion


    #region Private Method
    void Start()
    {

    }

    private void Update()
    {
        
    }

    private void GenerateJigsawTile(Texture2D texture)
    {

    }

    private IEnumerator ShuffleTile()
    {
        yield return new WaitForSeconds(1);
    }

    private void CheckClear()
    {

    }
    #endregion


    #region Public Method
    public override void Initialize()
    {
        base.Initialize();
        //_stageImage.sprite = Resources.Load<Sprite>("illust/stage/" + .Replace(".jpg", "").Replace(".png", ""));
    }


    #endregion

}
