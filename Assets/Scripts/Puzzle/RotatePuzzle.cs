using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RotatePuzzle : BaseLayerTemplate
{
    #region Inspector
    [Header("UI")]
    [SerializeField] private List<Sprite> _puzzleImage;
    [SerializeField] private Transform _mapSize;
    [Space(2)]

    [Header("Play Settings")]
    [SerializeField] float _longTapTime = 1.0f;
    [SerializeField] bool _useAnswerIllust = false;
    [SerializeField] bool _showQuestion = true;
    [SerializeField] bool _isFadeStageImage = false;
    #endregion

    #region Private FIeld
    Canvas _playcanvas;

    [SerializeField] GameObject _clonebase;
    [SerializeField] GameObject _tutorialDialog;
    [SerializeField] Button _closeBtn;
    [SerializeField] GameObject _stageImage;

    Image _cover;
    Color m_color;

    // Time Slider Parts
    Slider _timeSlider;

    Text _count;
    public bool _isTimerStop = true;
    float _countDown = 0.0f;
    float _tapTime;
    TouchManager _touchManager;
    StageData _stage;
    Section _section;

    List<GameObject> _puzzleList = new List<GameObject>();
    int _splitWidth = 3;
    int _splitHeight = 4;
    int _pazzleCount = 0;
    int _maxPazzle = 0;

    enum Rotate
    {
        Rotate_90 = 1,
        Rotate_180 = 2,
        Rotate_270 = 3,
        Rotate_MAX = 4
    }
    #endregion

    #region Public Field
    public bool IsFadeStageImage { get; private set; }
    public bool IsLongTap { get; set; }
    public bool IsTap { get; set; }

    public Timer GameTimer { get; set; }

    #endregion

    private void Awake()
    {
        IsFadeStageImage = _isFadeStageImage;
        _touchManager = new TouchManager();

        _closeBtn.onClick.AddListener(OnPointerBtnTutorialClose);
    }

    public void Initialize()
    {
        base.Initialize();
        // Set Data
        _stage = GameManager.Instance.PlayData[GameManager.Instance.SelectStageNumber];

        IsLongTap = false;
        IsTap = false;
        _tapTime = 0;
        _countDown = 20.0f;
        SetStageImage();
        CreatePazzle();
    }


    private void Update()
    {
#if UNITY_EDITOR

#else
        if (!_useAnswerIllust) {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && !IsTap) {
                IsTap = true;
                IsLongTap = false;
                _tapTime = 0;
            }

            if (touch.phase == TouchPhase.Ended && IsTap) {
                IsTap = false;
            }

            if (IsTap) {
                _tapTime += Time.deltaTime;
                if (_tapTime >= _longTapTime) {
                    IsLongTap = true;
                }
            }
        }
#endif
        // タッチ状態更新
        if (_touchManager == null || !GameManager.Instance.isPlaying) { return; }
        _touchManager.update();
    }

    void SetStageImage()
    {
        Sprite stageillust = Resources.Load<Sprite>("illust/stage/" + _stage.Section[0].AnswerImage.Replace(".png", ""));
        _stageImage.GetComponent<Image>().sprite = stageillust;
    }

    void CreatePazzle()
    {
        _splitWidth = _stage.Section[0].SliceNum;
        _splitHeight = _stage.Section[0].SliceNum;
        _maxPazzle = _splitWidth * _splitHeight;

        if (_puzzleList.Count != 0)
        {
            for (int i = 0; i < _puzzleList.Count; i++)
            {
                Destroy(_puzzleList[i]);
            }
            _puzzleList.Clear();
        }

        float pazzleWidth = _stageImage.gameObject.GetComponent<RectTransform>().sizeDelta.x / _splitWidth;
        float pazzleHeight = _stageImage.gameObject.GetComponent<RectTransform>().sizeDelta.y / _splitHeight;

        Vector2 pazzleSize = new Vector2(pazzleWidth, pazzleHeight);
        int _rotate_rate;

        _stageImage.GetComponent<GridLayoutGroup>().cellSize = pazzleSize;

        for (int i = 0; i < _splitHeight; i++)
        {
            for (int j = 0; j < _splitWidth; j++)
            {
                _rotate_rate = UnityEngine.Random.Range(1, 3);
                Vector2 pazzlePosition = new Vector2((pazzleWidth * j), (pazzleHeight * i));
                GameObject obj = Instantiate(_clonebase, _stageImage.transform);
                obj.GetComponent<Puzzle>().Setup(_stageImage.GetComponent<Image>().sprite.texture, pazzlePosition, Quaternion.Euler(0, 0, -(90 * _rotate_rate)), pazzleSize);
                _puzzleList.Add(obj);
            }
        }
        _puzzleList = _puzzleList.OrderBy(a => Guid.NewGuid()).ToList();
    }

    public void NextPazzle()
    {
        _pazzleCount++;
        if (_pazzleCount < _maxPazzle)
        {
            _puzzleList[_pazzleCount].SetActive(true);
            _puzzleList[_pazzleCount].transform.SetAsLastSibling();
        } else {
            Invoke("ShowClear", 0.3f);
        }
    }

    public void CheckAnswer()
    {
        int clear_count = 0;
        for (int count = 0; count < _puzzleList.Count; count++)
        {
            if (_puzzleList[count].gameObject.transform.rotation == Quaternion.Euler(0, 0, 0))
            {
                clear_count++;
            }
        }

        if (clear_count == _puzzleList.Count)
        {
            GameManager.Instance.IsClear = true;
            Invoke("ShowResultLayer", 0.5f);
        }
    }

    void OnPointerBtnTutorialClose()
    {
        _isTimerStop = false;
        _tutorialDialog.SetActive(false);

        int value = Convert.ToInt32(true);
        PlayerPrefs.SetInt("is_tutorial_played", value);
        PlayerPrefs.Save();
    }

    public void ShowResultLayer()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Result);
    }
}
