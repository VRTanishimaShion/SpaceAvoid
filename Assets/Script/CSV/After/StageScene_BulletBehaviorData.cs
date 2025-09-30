using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾の挙動を表すデータ
/// </summary>
public class StageScene_BulletBehaviorData
{
    /// <summary> キーワード </summary>
    public static string key { get; set; }

    /// <summary> 出現オフセット値 </summary>
    public string spawnTimeOffset {  get; set; }
    /// <summary> 予兆データ </summary>
    public string bulletPreEffect { get; set; }
    /// <summary> スピード </summary>
    public float bulletSpeed {  get; set; }

    /// <summary> コンストラクタ </summary>
    /// <param name="spawnTimeOffset"> 出現オフセット値 </param>
    /// <param name="bulletPreEffect"> 弾の予兆データ </param>
    /// <param name="bulletSpeed"> 弾のスピード </param>
    public StageScene_BulletBehaviorData(string spawnTimeOffset, string bulletPreEffect, float bulletSpeed)
    {
        this.spawnTimeOffset = spawnTimeOffset;
        this.bulletPreEffect = bulletPreEffect;
        this.bulletSpeed = bulletSpeed;
    }
}
