using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : BaseDialogTemplate
{
    [SerializeField] Button _soundBtn, _shareBtn, _closeBtn;

    // Start is called before the first frame update
    public virtual void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public virtual void Initialize()
    {
        _soundBtn.onClick.AddListener(OnClickSoundBtn);
        _shareBtn.onClick.AddListener(OnClickShareBtn);
        _closeBtn.onClick.AddListener(OnClickCloseBtn);
    }

    public virtual void UpdateData() { }

    private void OnClickSoundBtn()
    {
        if(GameManager.Instance.GetSoundOn()) {
            GameManager.Instance.SetSoundOn(false);
        } else {
            GameManager.Instance.SetSoundOn(true);
        }
    }

    private void OnClickShareBtn()
    {

    }
    
    private void OnClickCloseBtn()
    {
        this.gameObject.SetActive(false);
    }
}

