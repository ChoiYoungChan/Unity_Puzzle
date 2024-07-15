using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayLayer : BaseLayer
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
    [SerializeField] private Text _infoText;
    [SerializeField] private BaseLayer[] _puzzleObj;

    #endregion

    #region Private Field
    private string _stringFormat;
    private string _stagetype;
    #endregion

    #region Public Field

    public PuzzleType Type { get; set; } = PuzzleType.Sliding;

    #endregion

    private void Awake()
    {
        _stringFormat = _infoText.text;
    }

    #region Private Method
    private void OnEnable()
    {
        switch (Type)
        {
            case PuzzleType.JigSaw:
                _puzzleObj[(int)PuzzleType.JigSaw].gameObject.SetActive(true);
                _puzzleObj[(int)PuzzleType.Sliding].gameObject.SetActive(false);
                _puzzleObj[(int)PuzzleType.JigSaw].Initialize();
                _stagetype = "JigSaw Puzzle";
                break;

            case PuzzleType.Sliding:
                _puzzleObj[(int)PuzzleType.JigSaw].gameObject.SetActive(false);
                _puzzleObj[(int)PuzzleType.Sliding].gameObject.SetActive(true);
                _puzzleObj[(int)PuzzleType.Sliding].Initialize();
                _stagetype = "Sliding Puzzle";
                break;
            default:
                _puzzleObj[(int)PuzzleType.JigSaw].gameObject.SetActive(false);
                _puzzleObj[(int)PuzzleType.Sliding].gameObject.SetActive(true);
                _puzzleObj[(int)PuzzleType.Sliding].Initialize();
                _stagetype = "Sliding Puzzle";
                break;
        }
    }

    private void OnClickBackButton()
    {
        SoundManager.Instance.Play("button_click");
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.Select);
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
            DialogManager.Instance.OpenDialog(DialogManager.DialogKey.Setting);
        });
    }

    public void UpdateTimer()
    {
        // Timer

        _infoText.text = string.Format(_stringFormat, "", "");
    }

    #endregion

}
