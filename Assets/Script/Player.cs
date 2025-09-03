using UnityEngine;
using static Player;

/// <summary>
/// プレイヤーの管理
/// </summary>
public class Player : MonoBehaviour
{
    // 固定の数
    /// <summary>  </summary>

    // 変更可能な変数
    /// <summary> 速さ </summary>
    [SerializeField] private float PlayerSpeed = 3f;


    //////////////////////////////////////////////////////////////////////////
    /// <summary>  プレイヤーの入力を処理するスクリプト </summary>
    [SerializeField] private PlayerInputController _playerInputController;

    /// <summary>
    /// 動ける範囲
    /// </summary>
    public struct MovementRange
    {
        public float up;     // 上
        public float down;   // 下
        public float left;   // 左
        public float right;  // 右
    }
    private MovementRange _movementRange;


    /// <summary> Unityの機能の処理 </summary>
    public void InitSystem()
    {
        _playerInputController.InitSystem();
    }
    /// <summary> 変数の初期化など </summary>
    public void Init()
    {
        _movementRange = new MovementRange();
        float width = (transform.localScale.x / 2);
        _movementRange.up       = -width;
        _movementRange.down     = width;
        _movementRange.left     = width;
        _movementRange.right    = -width;

        _playerInputController.Init();
    }

    /// <summary>
    /// 動き
    /// </summary>
    public void Movement()
    {
        Vector2 inputVector = _playerInputController.GetInputVector();

        transform.Translate(inputVector * PlayerSpeed * Time.fixedDeltaTime);

        Vector2 position = transform.position;
        position.x = Mathf.Clamp(position.x, _movementRange.left, _movementRange.right);
        position.y = Mathf.Clamp(position.y, _movementRange.down, _movementRange.up);
        transform.position = position;
    }

    /// <summary>
    /// 動ける範囲を設定する
    /// </summary>
    /// <param name="range"> 壁の位置 </param>
    public void SetTheMovementRange(MovementRange range)
    {
        _movementRange.up += range.up;
        _movementRange.down += range.down;
        _movementRange.left += range.left;
        _movementRange.right += range.right;
    }
    /// <summary>
    /// 動ける範囲を取得
    /// </summary>
    /// <returns> 動ける範囲 </returns>
    public MovementRange GetTheMovementRange()
    {
        return _movementRange;
    }
}
