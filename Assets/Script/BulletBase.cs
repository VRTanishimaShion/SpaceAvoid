using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 球の基本となる設計(仮)
/// </summary>
public class BulletBase : MonoBehaviour
{
    // 弾の速度
    public float speed;
    // 角度
    public Vector2 flightAngle;

    /// <summary> Unityの機能の処理 </summary>
    public void InitSystem()
    {

    }
    /// <summary> 変数の初期化など </summary>
    public void Init(float speed, Vector2 launchAngle)
    {
        this.speed = speed;
        flightAngle = launchAngle;
    }

    /// <summary>
    /// 動き
    /// </summary>
    public void Movement()
    {
        transform.Translate(flightAngle * speed * Time.fixedDeltaTime);
    }
}
