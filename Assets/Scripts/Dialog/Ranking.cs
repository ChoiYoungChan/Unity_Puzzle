using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : BaseDialog
{
    [SerializeField] GameObject _listPrefab;
    [SerializeField] Button _clostBtn;

    private bool _isCreated = false;
    private GameObject _list;

    #region Private Method
    private void Start()
    {
        if(!_isCreated) CreateListPool();
    }

    // creat list object and put it in object pool first
    private void CreateListPool()
    {
        if (_list != null) Destroy(_list);


        _isCreated = true;
    }

    private void OnClickCloseBtn()
    {

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
        base.Initialize();
        _clostBtn.onClick.AddListener(()=> { OnClickCloseBtn(); });
    }

    public void ShowRankingList()
    {
        if (!this.gameObject.active) this.gameObject.SetActive(true);
    }

    #endregion


}
