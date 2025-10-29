using UnityEngine;

/// <summary>
/// 選択画面の管理
/// </summary>
public class StageSelectSceneGenerator : MonoBehaviour
{
    /// <summary> ゲームマネージャーのクラス </summary>
    [SerializeField] private StageButton _stageButton;
    /// <summary> ゲームマネージャーのクラス </summary>
    private GameManager _gameManager;

    /// <summary> Unityの機能の処理 </summary>
    public void InitSystem()
    {

    }
    /// <summary> 変数の初期化など </summary>
    public void Init()
    {

    }

    /// <summary> ゲームマネージャーのクラスを設定する </summary>
    /// <param name="gameManager"> ゲームマネージャーのクラス </param>
    public void SetTheGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
        _stageButton.SetTheGameManager(_gameManager);
    }
}
