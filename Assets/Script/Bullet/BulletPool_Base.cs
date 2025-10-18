using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 弾のプールオブジェクト（標準）
/// </summary>
public class BulletPool_Base : MonoBehaviour
{
    /// <summary> 弾の標準型プレファブ </summary>
    [SerializeField] private Bullet_Base bulletBasePrefab;
    /// <summary> 弾のステータス </summary>
    private Bullet_BaseStatus bulletStatus;
    /// <summary> 弾のプール </summary>
    private ObjectPool<Bullet_Base> bulletPool;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        bulletPool = new ObjectPool<Bullet_Base>(
                createFunc: () => InitObject(),
                actionOnGet: (obj) => InitializeObject(obj),
                actionOnRelease: (obj) => OnReleaseObject(obj),
                actionOnDestroy: (obj) => OnDestroyObject(obj),
                collectionCheck: true,
                defaultCapacity: 3,
                maxSize: 10
                );

    }

    /// <summary>
    /// プールからオブジェクトを取得する
    /// </summary>
    public Bullet_Base CreateTheObject(Bullet_BaseStatus BulletStatus)
    {
        bulletStatus = BulletStatus;
        return bulletPool.Get();
    }

    /// <summary>
    /// プールの中身を空にする
    /// </summary>
    public void ClearBullet_Base()
    {
        bulletPool.Clear();
    }

    /// <summary>
    /// プールに入れるインスタンスを新しく生成する際に行う処理
    /// </summary>
    /// <returns></returns>
    private Bullet_Base InitObject()
    {
        return Instantiate(bulletBasePrefab, transform);
    }

    /// <summary>
    /// プールからインスタンスを取得した際に行う処理
    /// </summary>
    /// <param name="enemyObject"> 敵のオブジェクト </param>
    private void InitializeObject(Bullet_Base bullet)
    {
        bullet.Initialize(bulletStatus,( () => bulletPool.Release(bullet)));
        bullet.gameObject.SetActive(true);
    }

    /// <summary>
    /// プールにインスタンスを返却した際に行う処理
    /// </summary>
    private void OnReleaseObject(Bullet_Base bullet)
    {
        // EnemyObject側で非アクティブにするのでログ出力のみ。ここで非アクティブにするパターンもある。
        Debug.Log("Release");
    }

    /// <summary>
    /// プールから削除される際に行う処理
    /// </summary>
    private void OnDestroyObject(Bullet_Base bullet)
    {
        Destroy(bullet.gameObject);
    }
}
