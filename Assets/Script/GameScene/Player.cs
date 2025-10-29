using System.Xml;
using UnityEngine;
using System.Collections.Generic;
using System;

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

    /// <summary> リジッドボディ </summary>
    private Rigidbody2D rb2d;

    // ステータス
    private const int MaxHitPoint = 3;  // 最大の体力
    private int currentHitPoint = 0;    // 現在の体力

    /// <summary> ゲームの終わりをつたえる変数 </summary>
    private Action gameFinish;
    
    //////////////////////////////////////////////////////////////////////////
    /// <summary>  プレイヤーの入力を処理するスクリプト </summary>
    [SerializeField] private PlayerInputController _playerInputController;
    /// <summary>  プレイヤーの入力を処理するスクリプト </summary>
    [SerializeField] private Player_HitPointUI _playerHitPointUI;


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
        rb2d = GetComponent<Rigidbody2D>();
    }
    /// <summary> 変数の初期化など </summary>
    public void Init()
    {
        currentHitPoint = MaxHitPoint;

        transform.position = Vector2.zero;

        _playerInputController.Init();
        _playerHitPointUI.ResetActive();
    }

    /// <summary>
    /// 動き
    /// </summary>
    public void Movement()
    {
        Vector2 inputVector = _playerInputController.GetInputVector();

        //if(inputVector == Vector2.zero)
        //{
        //    return;
        //}

        transform.Translate(inputVector * PlayerSpeed * Time.fixedDeltaTime);
        //rb2d.position = (rb2d.position + inputVector * PlayerSpeed * Time.fixedDeltaTime);

        Vector2 position = transform.position;
        position.x = Mathf.Clamp(position.x, _movementRange.left, _movementRange.right);
        position.y = Mathf.Clamp(position.y, _movementRange.down, _movementRange.up);
        transform.position = position;
    }

    /// <summary>
    /// 当たった瞬間
    /// </summary>
    /// <param name="other"> 当たったオブジェクト </param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(currentHitPoint <= 0)
        {
            return;
        }

        Bullet_Base bullet = other.GetComponent<Bullet_Base>();
        if(bullet != null)
        {
            if(!bullet.isCollider)
            {
                bullet.OnHit();
                OnHit();
            }
        }
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

        float width = (transform.localScale.x / 2);
        _movementRange.up += -width;
        _movementRange.down += width;
        _movementRange.left += width;
        _movementRange.right += -width;
    }

    /// <summary>
    /// 動ける範囲を取得
    /// </summary>
    /// <returns> 動ける範囲 </returns>
    public MovementRange GetTheMovementRange()
    {
        return _movementRange;
    }

    /// <summary>
    /// 当たった時の処理
    /// </summary>
    public void OnHit()
    {
        currentHitPoint -= 1;
        _playerHitPointUI.TakeDamage(currentHitPoint);

        if(currentHitPoint <= 0)
        {
            currentHitPoint = 0;
            _playerInputController.DoNotController();
            gameFinish?.Invoke();
            Debug.Log("Dead");
        }
    }

    /// <summary> ゲーム終了のアクションを挿入する </summary>
    public void TriggerGameOverEvent(Action GameFinish) { gameFinish = GameFinish; }
}
