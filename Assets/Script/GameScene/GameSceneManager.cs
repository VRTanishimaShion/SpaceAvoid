using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Player;

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
    /// <summary> プレイヤー </summary>
    [SerializeField] private Player _player;
    /// <summary> ゲーム全体の状態管理 </summary>
    private GameManager _gameManager;

    /// <summary> 弾のプールオブジェクトの管理 </summary>
    [SerializeField] private BulletPoolManager bulletPoolManager;

    

    /// <summary> ゲームが終わる </summary>
    private Action gameFinished;

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

        bulletPoolManager.SystemInit();
        
        // プレイヤーの初期化
        _player.InitSystem();
    }
    /// <summary> 変数の初期化など </summary>
    public void Init()
    {
        _stageGenerator.Init();
        
    }

    /// <summary>
    /// ゲームシーンの初期化
    /// </summary>
    public void GameSceneInit()
    {
        // プレイヤーの初期化
        _player.Init();
    }

    /// <summary> 終期化 </summary>
    public void GameScene_End()
    {
        bulletPoolManager.ClearPoolBulletObject();
    }

    /// <summary>
    /// メインデータを挿入する
    /// </summary>
    /// <param name="mainData"> メインデータ </param>
    public void SetTheMainData(MainData mainData)
    {
        bulletPoolManager.SetTheMainData(mainData);
    }

    /// <summary>
    /// ステージの番号を挿入する
    /// </summary>
    /// <param name="stageNumber"> ステージ番号 </param>
    public void SetTheStageNumber(int stageNumber)
    {
        bulletPoolManager.Init(stageNumber);
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

        bulletPoolManager.DSP();

        // 逆順させることで削除にも対応
        //for(int bulletNumber = bulletGenerator.Count - 1; bulletNumber >= 0; bulletNumber--)
        //{
        //    BulletBase bullet = bulletGenerator[bulletNumber];
        //    bullet.Movement(); 
        //}
    }

    /// <summary>
    /// 動きに関する周期化
    /// </summary>
    /// <param name="tm"> 今の時間 </param>
    public void GameSceneDeltaDPS(int tm)
    {
        _player.Movement();
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

    /// <summary> ゲーム終了のアクションを挿入する </summary>
    public void SetTheGameFinish(Action GameOver, Action GameClear) 
    { 
        _player.TriggerGameOverEvent(GameOver);
        bulletPoolManager.TriggerGameClearEvent(GameClear);
    }

    /// <summary>
    /// 動ける範囲を設定する
    /// </summary>
    public void SetTheMovementRange(MovementRange range)
    {
        _player.SetTheMovementRange(range);
        bulletPoolManager.SetTheMovementRange(range);
    }
}
