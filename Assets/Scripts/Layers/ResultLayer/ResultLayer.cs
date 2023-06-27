using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultLayer : BaseLayerTemplate
{
    [SerializeField] Button _nextBtn, _backBtn;
    [SerializeField] ParticleSystem[] _resultEffect;
    [SerializeField] Image _resultImg;
    [SerializeField] Sprite[] _resultSprite;

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
        _backBtn.onClick.AddListener(OnClickBackButton);
    }

    private void OnClickNextButton()
    {
        
    }

    private void OnClickBackButton()
    {

    }

    private void ShowResultImage()
    {

    }

    private void ShowResultEffect()
    {

    }
}
