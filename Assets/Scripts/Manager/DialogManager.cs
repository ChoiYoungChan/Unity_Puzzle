using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : SingletonClass<DialogManager>
{
    [SerializeField] GameObject[] _dialogList;

    public enum DialogKey {
        DialogKey_Setting = 0,
        DialogKey_Hint = 1,
        DialogKey_LogIn = 2,
        DialogKey_Ranking = 3,
        DialogKey_Max = 4
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenDialog(DialogKey _key)
    {
        _dialogList[(int)_key].gameObject.SetActive(true);
    }
}
