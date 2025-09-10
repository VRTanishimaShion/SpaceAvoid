using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームシーンの管理
/// </summary>
public class GameSceneManager : MonoBehaviour
{
    /// <summary>
    /// 弾の管理
    /// </summary>
    private List<BulletBase> bulletGenerator = new List<BulletBase>();

    /// <summary> ステージを管理するクラス </summary>
    [SerializeField] private StageGenerator _stageGenerator;
    /// <summary> 弾のオブジェクトを探すクラス </summary>
    [SerializeField] private BulletObjectFinder _bulletObjectFinder;
    /// <summary> ゲーム全体の状態管理 </summary>
    private GameManager _gameManager;

    /// <summary>
    /// 弾の情報
    /// </summary>
    public struct bulletInfo
    {
        public int time;
        public Vector2 spawnPosition;
        public Vector2 launchAngle;
    }
   
    /// <summary> Unityの機能の処理 </summary>
    public void InitSystem()
    {
        _stageGenerator.InitSystem();
    }
    /// <summary> 変数の初期化など </summary>
    public void Init()
    {
        _stageGenerator.Init();
    }

    /// <summary>
    /// ゲームシーンの周期化
    /// </summary>
    public void GameSceneDPS(int tm)
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            SpawnBullet(BulletObjectFinder.BulletType.Normal, new Vector2(-1, -1), 3f, new Vector2(0, 1));
        }

        foreach (BulletBase bullet in bulletGenerator)
        {
            bullet.Movement();
        }

        // 逆順させることで削除にも対応
        //for(int bulletNumber = bulletGenerator.Count - 1; bulletNumber >= 0; bulletNumber--)
        //{
        //    BulletBase bullet = bulletGenerator[bulletNumber];
        //    bullet.Movement();

            
        //}
    }

    /// <summary>
    /// 弾を生み出す
    /// </summary>
    public void SpawnBullet(BulletObjectFinder.BulletType bulletType,Vector2 spawnPosition, float speed, Vector2 launchAngle)
    {
        GameObject instance = Instantiate(_bulletObjectFinder.GetBullet(bulletType),spawnPosition,Quaternion.identity);
        BulletBase bullet = instance.GetComponent<BulletBase>();
        bullet.SetTheInit(speed, launchAngle);
        bulletGenerator.Add(bullet);
    }

    /// <summary>
    /// 動ける範囲を挿入する
    /// </summary>
    /// <param name="range"> 範囲 </param>
    public void SetTheMovementRange(ref Player.MovementRange range)
    {
        _stageGenerator.SetTheMovementRange(ref range);
    }

    /// <summary> ゲーム全体の状態管理を挿入 </summary>
    public void SetTheGameManager(GameManager gameManager) { _gameManager = gameManager;}
}
