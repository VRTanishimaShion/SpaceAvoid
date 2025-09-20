using UnityEngine;

/// <summary>
/// 一つの点滅パターン
/// </summary>
public class BlinkPattern
{
    /// <summary> 演出が続く時間 </summary>
    public int effectDurationFrames { get; private set; }
    /// <summary> 演出が続かない時間 </summary>
    public float effectIdleFrames { get; private set; }
}
