using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージシーン
/// </summary>
public class StageScene : MonoBehaviour
{
    /// <summary> キーワード </summary>
    public static string key { get; set; }

    /// <summary> フェーズ </summary>
    public List<string> phase = new List<string>();

    /// <summary> コンストラクタ </summary>
    /// <param name="Key"> キーワード </param>
    /// <param name="Phase"> フェーズ </param>
    public StageScene(string Key, List<string> Phase)
    {
        key = Key;
        phase = Phase;
    }

    /// <summary> フェーズの取得 </summary>
    public List<string> GetThePhase()
    {
        return phase;
    }
}
