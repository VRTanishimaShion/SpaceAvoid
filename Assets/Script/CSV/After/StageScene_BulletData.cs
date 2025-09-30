using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージシーンの弾データ
/// </summary>
public class StageScene_BulletData
{
    /// <summary> キーワード </summary>
    public static string key { get; set; }

    /// <summary> 弾の種類の関するデータ </summary>
    public string bulletAppearance {  get; set; }
    /// <summary> 挙動を表すデータ </summary>
    public string bulletBehavior {  get; set; }
    /// <summary> 座標 </summary>
    public string worldPosition { get; set; }
    /// <summary> 角度 </summary>
    public string angle { get; set; }
    /// <summary> フレーム </summary>
    public string frame {  get; set; }

    /// <summary> コンストラクタ </summary>
    public StageScene_BulletData(string Key, string BulletAppearance, string BulletBehavior, string WorldPosition, string Angle, string Frame)
    {
        key = Key;
        bulletAppearance = BulletAppearance;
        bulletBehavior = BulletBehavior;
        worldPosition = WorldPosition;
        angle = Angle;
        frame = Frame;
    }
}
