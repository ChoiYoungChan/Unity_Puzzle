using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections;

public class Loading : MonoBehaviour
{
    private bool _isDone;
    // Start is called before the first frame update
    private async void  Start()
    {
        await UniTask.WhenAll(LoadGameData(), MoveTo());
    }

    private async UniTask MoveTo()
    {
        _ = UniTask.WaitUntil(() => _isDone == true);

        await UniTask.Delay(3000);
        Debug.Log("## Move Top Layer");
    }

    private async UniTask LoadGameData()
    {
        await UniTask.Delay(3000); 
        Debug.Log("## Start Load Game Data from Server");
        _isDone = true;
    }
}
