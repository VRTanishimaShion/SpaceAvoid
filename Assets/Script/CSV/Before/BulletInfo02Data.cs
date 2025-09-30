using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 弾の情報０２
/// </summary>
public class BulletInfo02Data
{
    /// <summary> 出現オフセット値 </summary>
    /// <remarks>
    /// フェーズ開始時からのオフセットフレーム数
    /// </remarks>
    public float appearDelayFrames { get; private set; }
    /// <summary>
    /// 点滅パターンのリスト
    /// （赤表示 → 無表示 の繰り返し）
    /// </summary>
    public List<BlinkPattern> patterns { get; private set; }
    /// <summary> 速さ </summary>
    public float speed { get; private set; }


    public BulletInfo02Data()
    {

    }
}
