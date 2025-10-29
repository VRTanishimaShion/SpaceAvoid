using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾の予兆データ
/// </summary>
public class StageScene_BulletPreEffectData
{
    /// <summary> キーワード </summary>
    public string key { get; set; }

    /// <summary> 繧ｨ繝輔ぉ繧ｯ繝医�ｮ譎る俣 </summary>
    public List<float> effectTime;

    /// <summary> 繧ｳ繝ｳ繧ｹ繝医Λ繧ｯ繧ｿ </summary>
    public StageScene_BulletPreEffectData(string Key, List<float> EffectTime)
    {
        key = Key;
        effectTime = EffectTime;
    }

    /// <summary> 繧ｨ繝輔ぉ繧ｯ繝医�ｮ譎る俣 </summary>
    public List<float> GetTheEffectTime()
    {
        return effectTime;
    }
}
