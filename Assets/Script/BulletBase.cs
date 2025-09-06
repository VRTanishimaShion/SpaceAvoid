using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 球の基本となる設計(仮)
/// </summary>
public class BulletBase : MonoBehaviour
{
    // 弾の速度
    private float speed;
    // 角度
    private Vector2 flightAngle;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="speed"> 速さ </param>
    /// <param name="launchAngle"> 発射角度 </param>
    public BulletBase(float speed, Vector2 launchAngle)
    {
        this.speed = speed;
        flightAngle = launchAngle;
    }

    /// <summary> Unityの機能の処理 </summary>
    public void InitSystem()
    {

    }
    /// <summary> 変数の初期化など </summary>
    public void Init()
    {
        
    }

    /// <summary>
    /// 動き
    /// </summary>
    public void Movement()
    {
        transform.Translate(flightAngle * speed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// オブジェクトを削除
    /// </summary>
    public void DeleteObject()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="speed"> 速さ </param>
    /// <param name="launchAngle"> 発射角度 </param>
    public void SetTheInit(float speed, Vector2 launchAngle)
    {
        this.speed = speed;
        flightAngle = launchAngle;
    }
}
