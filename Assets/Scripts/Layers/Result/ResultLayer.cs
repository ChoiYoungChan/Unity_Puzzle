using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultLayer : BaseLayerTemplate
{
    #region Inspector
    [SerializeField] Button _nextBtn, _backBtn;
    [SerializeField] Image _resultImg;
    [SerializeField] Sprite[] _resultSprite;
    #endregion

    #region Private Field
    private bool canShake;
    private float _shakeTimer = 3;
    #endregion

    #region Private Method
    private void Update()
    {
        if (canShake) StartShakeEffect();
    }

    private void OnClickNextButton()
    {
        SoundManager.Instance.Play("button_click");
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Play);
    }

    private void OnClickBackButton()
    {
        SoundManager.Instance.Play("button_click");
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Top);
    }

    private void ShowResultImage()
    {
        _resultImg.sprite = GameManager.Instance.IsClear == true ? _resultSprite[0] : _resultSprite[1];
    }

    private void ShowResultEffect()
    {
        if (!GameManager.Instance.IsClear) canShake = true;
        DelayControlClass.Instance.CallAfter(1.0f, () => { _nextBtn.gameObject.SetActive(true); });
    }

    #endregion

    #region Public Method
    public virtual void Awake()
    {
        _nextBtn?.onClick.AddListener(OnClickNextButton);
        _backBtn?.onClick.AddListener(OnClickBackButton);
    }

    public virtual void OnEnable()
    {
        canShake = false;
        _nextBtn.gameObject.SetActive(false);

        _shakeTimer = 3;

        ShowResultImage();
        ShowResultEffect();
    }
    public void StartShakeEffect()
    {
        if (_shakeTimer > 0)
        {
            _resultImg.transform.localPosition = Vector3.zero + UnityEngine.Random.insideUnitSphere * 4.0f;
            _shakeTimer -= Time.deltaTime;
        }
        else
        {
            _shakeTimer = 0f;
            _resultImg.transform.localPosition = Vector3.zero;
            canShake = false;
        }
    }

    #endregion
}
