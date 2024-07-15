using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeAccount : BaseDialog
{
    [SerializeField] InputField _inputID;
    [SerializeField] InputField _inputPass;

    private void SendNewAccountInfo(string id, string pwd)
    {
        _ = NetWorkManager.Instance.MakeAccount(id, pwd);
    }

    private void Close()
    {
        this.GetComponent<MakeAccount>().Hide();
    }

    #region Public Method

    /// <summary>
    /// Initialize
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();

    }

    public void InputAccountInfo()
    {
        var id = _inputID.GetComponent<InputField>().text;
        var pwd = _inputPass.GetComponent<InputField>().text;

        try { if (id != null && pwd != null) SendNewAccountInfo(id, pwd); }
        catch (Exception e) { Debug.LogError("### Make Account Error " + e); }
    }

    #endregion
}
