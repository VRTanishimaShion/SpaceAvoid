using UnityEngine;

/// <summary>
/// ステージデータ
/// </summary>
public class StageData
{
    /// <summary> 番号 </summary>
    public int Id { get; private set; }
    /// <summary> 速さ </summary>
    public float speed { get; private set; }
    /// <summary> 速さ </summary>
    public float spawnTime { get; private set; }
    /// <summary> 速さ </summary>
    public Vector2 spawnPosition { get; private set; }
    /// <summary> 弾の角度 </summary>
    public Vector2 shootAngle { get; private set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public StageData(string[] values)
    {
        Id              = int.Parse(values[0]);
        speed           = float.Parse(values[1]);
        spawnTime       = float.Parse(values[2]);
        Vector2 vec     = CSVLoader.ParseVector2(values[3]);
        spawnPosition   = vec;
        vec             = CSVLoader.ParseVector2(values[4]);
        shootAngle      = vec;
    }
}
