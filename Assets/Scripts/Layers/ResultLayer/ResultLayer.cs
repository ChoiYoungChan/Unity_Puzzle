using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultLayer : BaseLayerTemplate
{
    [SerializeField] Button _nextBtn;
    [SerializeField] ParticleSystem[] _resultEffect;
    [SerializeField] Image _resultImg;
    [SerializeField] Sprite[] _resultBtnImage;
    [SerializeField] Sprite[] _resultImage;

    private bool b_IsClear;

    public virtual void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public virtual void Initialize()
    {
        _nextBtn.onClick.AddListener(OnClickNextButton);
    }

    private void Start()
    {
        // set result flag
        b_IsClear = GameManager.Instance.GetIsClear();

        // change button image after result flag set
        _nextBtn.GetComponent<Image>().sprite = (b_IsClear == true) ? _resultBtnImage[0] : _resultBtnImage[1];

        // show result image
        ShowResultImage();

        // start show result effect
        StartCoroutine(ShowResultEffect());
    }

    private void OnClickNextButton()
    {
        if (b_IsClear) { }
        else { }
    } 

    private void ShowResultImage()
    {
        _resultImg.sprite = (b_IsClear == true) ? _resultImage[0] : _resultImage[1];
    }

    IEnumerator ShowResultEffect()
    {
        if (b_IsClear)
        { }
        else
        { }


        yield return new WaitForSeconds(1.0f);
    }
}
