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
    [SerializeField] GameObject[] wallGenerator;

    /// <summary> Unityの機能の処理 </summary>
    public void InitSystem()
    {

    }
    /// <summary> 変数の初期化など </summary>
    public void Init()
    {

    }

    /// <summary>
    /// 動ける範囲を挿入する
    /// </summary>
    /// <param name="range"> 範囲 </param>
    public void SetTheMovementRange(ref Player.MovementRange range)
    {
        float width = (wallGenerator[0].transform.localScale.y / 2);

        range.up    = (wallGenerator[0].transform.position.y - width);
        range.down  = (wallGenerator[1].transform.position.y + width);
        range.left  = (wallGenerator[2].transform.position.x + width);
        range.right = (wallGenerator[3].transform.position.x - width);
    }
}
