using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : SingletonClass<DialogManager>
{
    [SerializeField] BaseDialog[] _dialogList;
    [SerializeField] Image _dialogFade;

    public enum DialogKey {
        Setting = 0,
        LogIn = 1,
        MakeAccount = 2,
        Ranking = 3
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenDialog(DialogKey _key)
    {
       /* DOTween.Sequence()
            .Append(_dialogFade.DOFade(0, 0))
            .Append(_dialogFade.DOFade(0.95f, 0.5f));*/
        _dialogList[(int)_key].gameObject.SetActive(true);
        var dialog = _dialogList[(int)_key] as BaseDialog;

        dialog.Initialize();
        dialog.Show();
    }

    public void HideDialog(DialogKey _key)
    {
        var dialog = _dialogList[(int)_key] as BaseDialog;

        DOTween.Sequence()
                   .Append(dialog.gameObject.transform.DOLocalMoveY(-2000, 0.5f));

        // DialogFade.
        DOTween.Sequence()
            .Append(_dialogFade.DOFade(0, 0.5f))
            .AppendCallback(() =>
            {
                _dialogFade.gameObject.SetActive(false);
            });
    }
}
