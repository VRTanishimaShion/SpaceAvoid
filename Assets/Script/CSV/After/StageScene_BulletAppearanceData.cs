using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾の種類の関するデータ
/// </summary>
public class StageScene_BulletAppearanceData
{
    /// <summary> キーワード </summary>
    public string key { get; set; }

    /// <summary> 弾の種類 </summary>
    public string bulletType { get; set; }
    /// <summary> 弾の大きさ </summary>
    public Vector2 bulletScale { get; set; }

    /// <summary> コンストラクタ </summary>
    /// <param name="bulleType"> 種類 </param>
    /// <param name="bulletScale"> 大きさ </param>
    public StageScene_BulletAppearanceData(string Key, string BulletType, Vector2 BulletScale)
    {
        key = Key;
        bulletType = BulletType;
        bulletScale = BulletScale;
    }
}
