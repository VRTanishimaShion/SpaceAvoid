using UnityEngine;

/// <summary>
/// 弾のステータス情報（標準）
/// </summary>
public class Bullet_BaseStatus
{
    public Sprite sprite;               // 見た目
    public Vector2 scale;               // 大きさ
    public float spawnTimeOffset;       // 出現オフセット
    public float[] preEffect;           // 予兆データ
    public float speed;                 // 速さ
    public Vector2 worldPosition;       // ワールドの位置
    public Vector2 angle;               // 角度
    //枠データ（予定）

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Bullet_BaseStatus(
        Sprite sprite,
        Vector2 scale,
        float spawnTimeOffset,
        float[] preEffect,
        float speed,
        Vector2 worldPosition,
        Vector2 angle
    )
    {
        this.sprite = sprite;
        this.scale = scale;
        this.spawnTimeOffset = spawnTimeOffset;
        this.preEffect = preEffect;
        this.speed = speed;
        this.worldPosition = worldPosition;
        this.angle = angle;
    }
}
