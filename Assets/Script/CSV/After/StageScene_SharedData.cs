using UnityEngine;

/// <summary>
/// ステージシーンの共有データ
/// </summary>
public class StageScene_SharedData
{
    /// <summary> キーワード </summary>
    public string key { get; set; }
    
    /// <summary> パラメータ </summary>
    public int parameter { get; set; }

    /// <summary> コンストラクタ </summary>
    public StageScene_SharedData(string Key, int Parameter)
    {
        key = Key;
        parameter = Parameter;
    }
}
