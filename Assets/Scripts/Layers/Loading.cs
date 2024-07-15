using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections;

public class Loading : BaseLayer
{
    private bool _isDone;
    // Start is called before the first frame update
    private async void  Start()
    {
        //await UniTask.WhenAll(LoadGameData(), LoadPlayerData());
        await LoadGameData();

        MoveLayer();
    }

    private async UniTask LoadPlayerData()
    {
        await UniTask.Delay(3000);
        Debug.Log("## LoadPlayerData");
    }

    private async UniTask LoadGameData()
    {
        await UniTask.Delay(1000); 
        Debug.Log("## Load Game Data");
        _isDone = true;
    }

    public override void MoveLayer()
    {
        //_ = UniTask.WaitUntil(() => _isDone == true);

        Debug.Log("## Move TopLayer");
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.Top);
    }
}
