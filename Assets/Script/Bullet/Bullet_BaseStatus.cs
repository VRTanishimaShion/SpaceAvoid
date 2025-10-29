using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 弾のステータス情報（標準）
/// </summary>
public class Bullet_BaseStatus
{
    public Sprite sprite;               // 見た目
    public Vector2 scale;               // 大きさ
    public float spawnTimeOffset;       // 出現オフセット
    public List<float> preEffect;           // 予兆データ
    public float speed;                 // 速さ
    public Vector2 worldPosition;       // ワールドの位置
    public Vector2 angle;               // 角度
    public Player.MovementRange range;  // 枠

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Bullet_BaseStatus(
        Sprite sprite,
        Vector2 scale,
        float spawnTimeOffset,
        List<float> preEffect,
        float speed,
        Vector2 worldPosition,
        Vector2 angle,
        Player.MovementRange range

    )
    {
        this.sprite = sprite;
        this.scale = scale;
        this.spawnTimeOffset = spawnTimeOffset;
        this.preEffect = preEffect;
        this.speed = speed;
        this.worldPosition = worldPosition;
        this.angle = angle;
        this.range = range;
    }
}
