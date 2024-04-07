using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayLayer : BaseLayerTemplate
{
    #region Enum
    public enum PuzzleType
    {
        Sliding = 0,
        JigSaw = 1
    }
    #endregion

    #region Inspector

    [SerializeField] private Button _backBtn, _settingBtn;
    [SerializeField] private Text _stagenameText;
    [SerializeField] private BaseLayerTemplate[] _puzzleObj;

    #endregion

    #region Public Field

    public PuzzleType Type { get; set; } = PuzzleType.Sliding;

    #endregion

    #region Private Method
    private void OnEnable()
    {
        string stageText = "";
        switch (Type)
        {
            case PuzzleType.JigSaw:
                stageText = "JigSaw Puzzle";
                _puzzleObj[(int)PuzzleType.JigSaw].gameObject.SetActive(true);
                _puzzleObj[(int)PuzzleType.Sliding].gameObject.SetActive(false);
                _puzzleObj[(int)PuzzleType.JigSaw].Initialize();
                break;

            case PuzzleType.Sliding:
                stageText = "Sliding Puzzle";
                _puzzleObj[(int)PuzzleType.JigSaw].gameObject.SetActive(false);
                _puzzleObj[(int)PuzzleType.Sliding].gameObject.SetActive(true);
                _puzzleObj[(int)PuzzleType.Sliding].Initialize();
                break;
            default:
                stageText = "Sliding Puzzle";
                _puzzleObj[(int)PuzzleType.JigSaw].gameObject.SetActive(false);
                _puzzleObj[(int)PuzzleType.Sliding].gameObject.SetActive(true);
                _puzzleObj[(int)PuzzleType.Sliding].Initialize();
                break;
        }
        _stagenameText.text = stageText;
    }

    private void OnClickBackButton()
    {
        SoundManager.Instance.Play("button_click");
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Select);
    }

    #endregion

    #region Public Method

    /// <summary>
    /// Initialize
    /// </summary>
    public override void Initialize()
    {
        _backBtn.onClick.AddListener(OnClickBackButton);
        _settingBtn.onClick.AddListener(() => {
            DialogManager.Instance.OpenDialog(DialogManager.DialogKey.DialogKey_Setting);
        });
    }

    #endregion

}
