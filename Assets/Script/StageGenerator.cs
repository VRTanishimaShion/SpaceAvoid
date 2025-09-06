using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ステージの管理
/// </summary>
public class StageGenerator : MonoBehaviour
{
    // 固定の数
    /// <summary>  </summary>

    // 変更可能な変数
    /// <summary>  </summary>


    //////////////////////////////////////////////////////////////////////////
    /// <summary> 壁（オブジェクト）の管理 </summary>
    [SerializeField] private GameObject[] wallGenerator;
    /// <summary> ステージの範囲 </summary>
    private Player.MovementRange stageRange;

    /// <summary> Unityの機能の処理 </summary>
    public void InitSystem()
    {
        
    }
    /// <summary> 変数の初期化など </summary>
    public void Init()
    {
        stageRange = new Player.MovementRange();

        float width = (wallGenerator[0].transform.localScale.y / 2);
        stageRange.up = (wallGenerator[0].transform.position.y - width);
        stageRange.down = (wallGenerator[1].transform.position.y + width);

        width = (wallGenerator[2].transform.localScale.x / 2);
        stageRange.left = (wallGenerator[2].transform.position.x + width);
        stageRange.right = (wallGenerator[3].transform.position.x - width);
    }

    /// <summary>
    /// 動ける範囲を挿入する
    /// </summary>
    /// <param name="range"> 範囲 </param>
    public void SetTheMovementRange(ref Player.MovementRange range)
    {
        range = stageRange;
    }
    /// <summary>
    /// ステージの範囲を返す
    /// </summary>
    /// <returns> ステージの範囲 </returns>
    public Player.MovementRange GetTheMovementRange()
    {
        return stageRange;
    }
}
