using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾の種類の関するデータ
/// </summary>
public class StageScene_BulletAppearanceData
{
    /// <summary> キーワード </summary>
    public static string key { get; set; }

    /// <summary> 弾の種類 </summary>
    public string bulleType { get; set; }
    /// <summary> 弾の大きさ </summary>
    public string bulletScale { get; set; }

    /// <summary> コンストラクタ </summary>
    /// <param name="bulleType"> 種類 </param>
    /// <param name="bulletScale"> 大きさ </param>
    public StageScene_BulletAppearanceData(string bulleType, string bulletScale)
    {
        this.bulleType = bulleType;
        this.bulletScale = bulletScale;
    }
}
