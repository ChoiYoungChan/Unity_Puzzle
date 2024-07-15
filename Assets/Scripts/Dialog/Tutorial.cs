using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : BaseDialog
{
    [SerializeField] Button _closeBtn;
    [SerializeField] GameType _type;

    public enum GameType
    {
        JigSawPuzzle = 0,
        SlidPuzzle = 1,
        Max = 2
    }

    private void Start()
    {
        _closeBtn.onClick.AddListener(() => {
            CacheData.Instance.Tutorial = true;
            this.gameObject.SetActive(false);
        });
    }

    private void StartTutorial()
    {
        switch(_type)
        {
            case GameType.JigSawPuzzle:
                JigSawTutorial();
                break;
            case GameType.SlidPuzzle:
                SlidTutorial();
                break;
            default:
                JigSawTutorial();
                break;
        }
    }

    private void JigSawTutorial()
    {

    }

    private void SlidTutorial()
    {

    }

}
