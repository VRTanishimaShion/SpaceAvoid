using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Globalization;

/// <summary>
/// 弾のプールオブジェクトの管理
/// </summary>
public class BulletPoolManager : MonoBehaviour
{
    /// <summary> 標準型 </summary>
    [SerializeField] private BulletPool_Base bulletPool_Base;

    /// <summary> 選択ステージ番号 </summary>
    private int selectStageNumber = -1;

    /// <summary> 動きに関する弾 </summary>
    private List<Bullet_Base> movingBullet = new List<Bullet_Base>();


    ////////// データ関係 /////////////
    /// <summary> メインデータ </summary>
    private MainData mainData;
    public void SetTheMainData(MainData MainData) { mainData = MainData; }
    /// <summary> フェーズ番号 </summary>
    private List<string> phases = new List<string>();
    /// <summary> 現在のフェーズ番号 </summary>
    private int nowPhasesNumber = -1;

    //////////// 後で変更する部分 ////////////////
    /// <summary> 弾の画像 </summary>
    [SerializeField] private Sprite sprite;
    /// <summary> 弾の状態 </summary>
    private Bullet_BaseStatus bulletTestStatus;
    /// <summary>
    /// テスト用の予兆データ
    /// </summary>
    private float[] testPreEffect = new float[] { 3,3,1,1 };

    /// <summary>
    /// 初期化
    /// </summary>
    public void Init(int SelectStageNumber)
    {
        // 初期化
        bulletPool_Base.Initialize();

        selectStageNumber = SelectStageNumber;
        nowPhasesNumber = 0;

        // 仮のバレット
        bulletTestStatus = new Bullet_BaseStatus(
            sprite,
            new Vector2(1, 1),
            1.0f,
            testPreEffect,
            4f,
            Vector2.zero,
            new Vector2(0, 1)
            );

        SetPhaseNumber();
    }

    /// <summary>
    /// 周期化
    /// </summary>
    public void DSP()
    {
        // プールオブジェクト内でActive状態がない場合は次の生成
        // 現在盤面に弾があるかを確認する
        // ない場合はフェーズを呼び弾を作成する
        if (movingBullet.Count == 0)
        {
            CreatePhaseObject();
            return;
        }

        // プールオブジェクト内でActive状態がある場合は動かす
        for (int i = (movingBullet.Count - 1); i >= 0; i--)
        {
            if (movingBullet[i].isBreak)
            {
                movingBullet[i].CallBreak();
                movingBullet.RemoveAt(i);

                continue;
            }

            movingBullet[i].Movement();
        }
    }

    /// <summary> フェーズを挿入する </summary>
    public void SetPhaseNumber()
    {
        phases = mainData.GetThePhases(selectStageNumber);
    }

    /// <summary>
    /// フェーズのオブジェクトを作成する
    /// </summary>
    public void CreatePhaseObject()
    {
        /// メインデータから読み込んだ上での生成 ///
        // 現在のフェーズ番号から弾の番号を受け取る
        List<string> bulletsInPhase = mainData.GetTheBulletsInPhase(phases[nowPhasesNumber]);
        //次のフェーズ番号を挿入する
        nowPhasesNumber = (nowPhasesNumber + 1) % phases.Count;



        // 弾の番号から弾の生成を行う
        if (bulletsInPhase == null) return;

        foreach(string bullet in bulletsInPhase)
        {
            //Bullet_BaseStatus bulletStatus = mainData.GetTheBullet(bullet);

            //// 仮
            //bulletStatus.sprite = sprite;
            //bulletStatus.preEffect = testPreEffect;

            bulletTestStatus.worldPosition = new Vector2(Random.Range(-5f, 5f), Random.Range(0f, 3f));

            Bullet_Base obj = bulletPool_Base.CreateTheObject(bulletTestStatus);
            movingBullet.Add(obj);
        }

    }

    /// <summary>
    /// 弾のプールオブジェクトをクリアする
    /// </summary>
    public void ClearPoolBulletObject()
    {
        bulletPool_Base.ClearBullet_Base();
    }
}
