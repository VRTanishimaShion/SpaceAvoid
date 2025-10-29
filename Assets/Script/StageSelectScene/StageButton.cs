using UnityEngine;

/// <summary>
/// ステージのボタン
/// </summary>
public class StageButton : MonoBehaviour
{
    /// <summary> ゲームマネージャーのクラス </summary>
    private GameManager _gameManager;

    /// <summary>
    /// ステージボタンが押されたとき
    /// </summary>
    /// <param name="stageNumber"> ステージ番号 </param>
    public void OnStageButton(int stageNumber)
    {
        if(stageNumber < 0)
        {
            Debug.LogError("存在しない番号");
        }
        _gameManager.SetTheStageNumber(stageNumber);
    }

    /// <summary> ゲームマネージャーのクラスを設定する </summary>
    /// <param name="gameManager"> ゲームマネージャーのクラス </param>
    public void SetTheGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}
