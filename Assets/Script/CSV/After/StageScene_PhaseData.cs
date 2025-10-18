using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージシーンのフェーズデータ
/// </summary>
public class StageScene_PhaseData
{
    /// <summary> キーワード </summary>
    public string key { get; set; }

    /// <summary> 弾 </summary>
    public List<string> bullet;

    /// <summary> コンストラクタ </summary>
    /// <param name="Key"> キーワード </param>
    /// <param name="Bullet"> 弾 </param>
    public StageScene_PhaseData(string Key, List<string> Bullet)
    {
        key = Key;
        bullet = Bullet;
    }

    /// <summary> 弾の取得 </summary>
    public List<string> GetTheBullet()
    {
        return bullet;
    }
}
