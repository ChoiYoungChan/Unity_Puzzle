using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : BaseDialog
{
    [SerializeField] InputField _inputID;
    [SerializeField] InputField _inputPass;

    [SerializeField] GameObject _makeAccountDlg;
    [SerializeField] Button _makeaccountBtn;
    [SerializeField] Button _googleLoginBtn;
    [SerializeField] Button _loginBtn;
    [SerializeField] Button _closeBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LoginAccount(string id, string pwd)
    {
        _ = NetWorkManager.Instance.LogIn(id, pwd);
    }

    private void OnClickMakeAccountBtn()
    {
        _makeAccountDlg.SetActive(true);
        var dialog = _makeAccountDlg.GetComponent("MakeAccount") as BaseDialog;

        dialog.Initialize();
        dialog.Show();
    }

    private void Close()
    {
        this.GetComponent<Login>().Hide();
    }

    public override void Initialize()
    {
        _makeaccountBtn.onClick.AddListener(()=> { OnClickMakeAccountBtn(); });
        _googleLoginBtn.onClick.AddListener(()=> { LoginGoogleAccount(); });
        _loginBtn.onClick.AddListener(()=> { InputAccountInfo(); });
        _closeBtn.onClick.AddListener(()=> { Close(); });
    }

    public void InputAccountInfo()
    {
        var id = _inputID.GetComponent<InputField>().text;
        var pwd = _inputPass.GetComponent<InputField>().text;

        try { if (id != null && pwd != null) LoginAccount(id, pwd); }
        catch (Exception e) { Debug.LogError("### Login Error " + e); }
    }

    public void LoginGoogleAccount()
    {
        //NetWorkManager.Instance.LogIn(_id, _password);
    }
}
