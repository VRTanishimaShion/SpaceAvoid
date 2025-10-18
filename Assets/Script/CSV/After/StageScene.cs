using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージシーン
/// </summary>
public class StageScene
{
    /// <summary> キーワード </summary>
    public string key { get; set; }

    /// <summary> フェーズ </summary>
    public List<string> phase = new List<string>();
    /// <summary> フェーズ </summary>
    public int neededClearCount;

    /// <summary> コンストラクタ </summary>
    public StageScene(string Key, List<string> Phase, int NeededClearCount)
    {
        key = Key;
        phase = Phase;
        neededClearCount = NeededClearCount;
    }

    /// <summary> フェーズの取得 </summary>
    public List<string> GetThePhase()
    {
        return phase;
    }
}
