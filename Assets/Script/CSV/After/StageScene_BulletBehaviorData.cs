using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾の挙動を表すデータ
/// </summary>
public class StageScene_BulletBehaviorData
{
    /// <summary> キーワード </summary>
    public string key { get; set; }

    /// <summary> 出現オフセット値 </summary>
    public float spawnTimeOffset {  get; set; }
    /// <summary> 予兆データ </summary>
    public string bulletPreEffect { get; set; }
    /// <summary> スピード </summary>
    public float bulletSpeed {  get; set; }

    /// <summary> コンストラクタ </summary>
    /// <param name="spawnTimeOffset"> 出現オフセット値 </param>
    /// <param name="bulletPreEffect"> 弾の予兆データ </param>
    /// <param name="bulletSpeed"> 弾のスピード </param>
    public StageScene_BulletBehaviorData(string Key, float SpawnTimeOffset, string BulletPreEffect, float BulletSpeed)
    {
        key = Key;
        spawnTimeOffset = SpawnTimeOffset;
        bulletPreEffect = BulletPreEffect;
        bulletSpeed = BulletSpeed;
    }
}
