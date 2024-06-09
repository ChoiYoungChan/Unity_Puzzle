using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : BaseDialogTemplate
{
    #region Inspector
    [SerializeField] Button _soundBtn, _shareBtn, _closeBtn;
    [SerializeField] Sprite _soundOn, _soundOff;

    #endregion

    #region Private Method
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        _soundBtn.GetComponent<Image>().sprite = GameManager.Instance.IsSoundOn ? _soundOn : _soundOff;
        SoundManager.Instance.Play("bgm");
    }


    private void OnClickSoundBtn()
    {
        if (GameManager.Instance.IsSoundOn)
        {
            GameManager.Instance.IsSoundOn = false;
            SoundManager.Instance.Pause();
        }
        else
        {
            GameManager.Instance.IsSoundOn = true;
            SoundManager.Instance.Play("bgm");
        }

        _soundBtn.GetComponent<Image>().sprite = (GameManager.Instance.IsSoundOn == true) ? _soundOn : _soundOff;
    }

    private void OnClickShareBtn()
    {

    }

    private void OnClickCloseBtn()
    {
        Time.timeScale = 1.0f;
        this.gameObject.SetActive(false);
    }
    #endregion

    #region Public Method
    public override void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public override void Initialize()
    {
        _soundBtn.onClick.AddListener(OnClickSoundBtn);
        _shareBtn.onClick.AddListener(OnClickShareBtn);
        _closeBtn.onClick.AddListener(OnClickCloseBtn);
    }

    public override void UpdateData() { }

    #endregion
}

