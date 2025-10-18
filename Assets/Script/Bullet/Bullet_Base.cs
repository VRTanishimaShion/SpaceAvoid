using JetBrains.Annotations;
using System;
using UnityEngine;

/// <summary>
/// 弾の標準型
/// </summary>
public class Bullet_Base : MonoBehaviour
{
    /// <summary> スプライトレンダラー </summary>
    [SerializeField] private SpriteRenderer spriteRenderer;

    /// <summary> 枠の位置 </summary>
    private FramePosition framePos = new FramePosition();

    /// <summary> 非アクティブ化するためのコールバック </summary>
    private Action _onDisable;

    /// <summary> 壊れている </summary>
    public bool isBreak{ get; private set; }
    
    /// <summary> 出現オフセット値 </summary>
    private float spawnTimeOffset;
    /// <summary> 予兆データ </summary>
    private float[] preEffect;
    /// <summary> 速さ </summary>
    private float speed;
    /// <summary> 角度 </summary>
    private Vector2 angle;
    
    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="bulletStatus"> 弾のステータス </param>
    public void Initialize(Bullet_BaseStatus bulletStatus, Action onDisable)
    {
        spriteRenderer.sprite = bulletStatus.sprite;
        transform.localScale = bulletStatus.scale;
        spawnTimeOffset = bulletStatus.spawnTimeOffset;
        preEffect = bulletStatus.preEffect;
        speed = bulletStatus.speed;
        transform.position = bulletStatus.worldPosition;
        angle = bulletStatus.angle;

        _onDisable = onDisable;
        isBreak = false;

        // テスト用
        framePos.up = 5;
        framePos.down = -5;
        framePos.left = -5;
        framePos.right = 5;
    }

    /// <summary>
    /// 動きに関するもの
    /// </summary>
    public void Movement()
    {
        Vector2 pos = transform.position;
        pos = pos + (GetMoveDirection() * speed * Time.deltaTime);
        transform.position = pos;

        if (pos.y >= framePos.up || pos.y <= framePos.down ||
            pos.x >= framePos.right || pos.x <= framePos.left)
        {
            Break();
        }
    }

    /// <summary>
    /// 動きの向きに関する制御
    /// </summary>
    /// <returns> 向き </returns>
    public Vector2 GetMoveDirection()
    {
        Vector2 direction = angle;

        return direction;
    }

    /// <summary>
    /// 壊れる
    /// </summary>
    public void Break()
    {
        if (_onDisable == null) return;

        _onDisable?.Invoke();
        isBreak = true;

        _onDisable = null;
    }

    /// <summary>
    /// 読んで壊す処理
    /// </summary>
    public void CallBreak()
    {
        gameObject.SetActive(false);
    }
}
