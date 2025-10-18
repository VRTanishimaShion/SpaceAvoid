using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// csvの全ての読み込み場所
/// </summary>
public class MainData : MonoBehaviour
{
    //////////  ステージシーン  //////////
    /// <summary> 共有データ </summary>
    private List<StageScene_SharedData> stageScene_SharedData = new List<StageScene_SharedData>();
    /// <summary> ステージシーン </summary>
    private List<StageScene> stageSceneData = new List<StageScene>();
    /// <summary> フェーズデータ </summary>
    private List<StageScene_PhaseData> stageScene_PhasesData = new List<StageScene_PhaseData>();
    /// <summary> 弾のデータ </summary>
    private List<StageScene_BulletData> stageScene_BulletData = new List<StageScene_BulletData>();
    /// <summary> 弾の形のデータ </summary>
    private List<StageScene_BulletAppearanceData> stageScene_BulletAppearanceData = new List<StageScene_BulletAppearanceData>();
    /// <summary> 弾の挙動のデータ </summary>
    private List<StageScene_BulletBehaviorData> stageScene_BulletBehaviorData = new List<StageScene_BulletBehaviorData>();
    /// <summary> 弾の挙動のデータ </summary>
    private List<StageScene_BulletPreEffectData> stageScene_BulletPreEffectData = new List<StageScene_BulletPreEffectData>();


    /// <summary>
    /// 初期化
    /// </summary>
    public void MainDataInit()
    {
        ReadStageSceneData();
    }

    /// <summary>
    /// ステージシーン
    /// </summary>
    private async Task ReadStageSceneData()
    {
        // 共有データ
        string[][] sharedData = await CSVReader.LoadCSVData("SymDef.csv");

        if(sharedData != null)
        {
            for(int i = 1; i < sharedData.GetLength(0); i++)
            {
                stageScene_SharedData.Add(new StageScene_SharedData(sharedData[i][0], int.Parse(sharedData[i][1])));
            }
        }
        else
        {
            Debug.Log("共有データの情報が取れていない");
        }

        // ステージデータ
        string[][] stageData = await CSVReader.LoadCSVData("StageData.csv");

        int nowPhasesNumber = 0;
        string baseName = "PhsDt_";
        if (stageData != null)
        {
            for (int i = 1; i < stageData.GetLength(0); i++)
            {
                List<string> phases = new List<string>();
                string phaseName = stageData[i][1];
                string numberPart = phaseName.Split('_')[1];
                int phaseNumber = int.Parse(numberPart);

                for( ; nowPhasesNumber <= phaseNumber; nowPhasesNumber++)
                {
                    phases.Add((baseName) + (nowPhasesNumber.ToString("D2")));
                }
                
                stageSceneData.Add(new StageScene(stageData[i][0], phases, int.Parse(stageData[i][2])));

            }
        }
        else
        {
            Debug.LogError("ステージデータの情報が取れていない");
        }

        // フェーズデータ
        string[][] phasesData = await CSVReader.LoadCSVData("PhaseData.csv");

        if (phasesData != null)
        {
            for(int i = 1; i < phasesData.GetLength(0);i++)
            {
                List<string> bullets = new List<string>();
                bullets = phasesData[i].ToList<string>();
                bullets.Remove(phasesData[i][0]);

                stageScene_PhasesData.Add(new StageScene_PhaseData(phasesData[i][0], bullets));
            }
        }
        else
        {
            Debug.LogError("フェーズデータの情報が取れていない");
        }

        // 弾のデータ
        string[][] bulletData = await CSVReader.LoadCSVData("BulletData.csv");
        if(bulletData != null)
        {
            for(int i = 1; i < bulletData.GetLength(0); i++)
            {
                float positionX = 0, positionY = 0;
                Vector2 angle = Vector2.zero;

                ///シンボル定義されているか検索する
                // 位置
                StageScene_SharedData x = stageScene_SharedData.FirstOrDefault(p => p.key == bulletData[i][3]);
                StageScene_SharedData y = stageScene_SharedData.FirstOrDefault(p => p.key == bulletData[i][4]);

                positionX = (x != null) ? x.parameter : int.Parse(bulletData[i][3]);
                positionY = (y != null) ? y.parameter : int.Parse(bulletData[i][4]);

                float degrees = float.Parse(bulletData[i][5]);
                float radians = degrees * Mathf.Deg2Rad;

                // 角度
                angle = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
                
                stageScene_BulletData.Add(
                    new StageScene_BulletData
                    (
                    bulletData[i][0],
                    bulletData[i][1],
                    bulletData[i][2],
                    new Vector2(positionX, positionY),
                    angle,
                    bulletData[i][6]
                    )
                );
            }
        }
        else
        {
            Debug.LogError("弾のデータの情報が取れていない");
        }

        // 弾の種類のデータ(弾データ①)
        string[][] bulletAppearanceData = await CSVReader.LoadCSVData("BltAttDt_1.csv");
        if(bulletAppearanceData != null)
        {
            for(int i = 1; i < bulletAppearanceData.GetLength(0);i++)
            {
                Vector2 scale = new Vector2(float.Parse(bulletAppearanceData[i][2]), float.Parse(bulletAppearanceData[i][3]));

                stageScene_BulletAppearanceData.Add(
                    new StageScene_BulletAppearanceData(
                        bulletAppearanceData[i][0],
                        bulletAppearanceData[i][1],
                        scale
                    )
                );
            }
        }
        else
        {
            Debug.LogError("弾の種類のデータの情報が取れていない");
        }

        // 弾の挙動を表すデータ(弾データ②)
        string[][] bulletBehaviorData = await CSVReader.LoadCSVData("BltAttDt_2.csv");
        if(bulletBehaviorData != null)
        {
            for(int i = 1; i < bulletBehaviorData.GetLength(0); i++)
            {
                float bulletSpeed = 0;

                ///シンボル定義されているか検索する
                StageScene_SharedData speed = stageScene_SharedData.FirstOrDefault(p => p.key == bulletBehaviorData[i][3]);

                bulletSpeed = (speed != null) ? speed.parameter : float.Parse(bulletBehaviorData[i][3]);

                stageScene_BulletBehaviorData.Add(
                    new StageScene_BulletBehaviorData(
                        bulletBehaviorData[i][0],
                        float.Parse(bulletBehaviorData[i][1]),
                        bulletBehaviorData[i][2],
                        bulletSpeed
                    )
                );
                
            }
        }
        else
        {
            Debug.LogError("弾の挙動を表すデータの情報が取れていない");
        }
    }

    /// <summary>
    /// 選択されたステージ番号でフェーズを返す
    /// </summary>
    /// <param name="selectStageNumber"> ステージ番号 </param>
    /// <returns> フェーズ番号 </returns>
    public List<string> GetThePhases(int selectStageNumber)
    {
        return stageSceneData[selectStageNumber].phase;
    }

    /// <summary>
    /// フェーズ内の弾データ(string)を取得
    /// </summary>
    /// <param name="phaseNumber"> フェーズ番号 </param>
    /// <returns> 
    /// 成功 : 弾データを取得できる
    /// 失敗 : null
    /// </returns>
    public List<string> GetTheBulletsInPhase(string phaseNumber)
    {
        StageScene_PhaseData phaseData = stageScene_PhasesData.FirstOrDefault(p=> p.key == phaseNumber);

        if(phaseData != null)
        {
            return phaseData.GetTheBullet();
        }
        else
        {
            Debug.Log("フェーズデータ内のバレットの番号(string)が受け取れない");
        }

        return null;
    }

    /// <summary>
    /// 弾を取得する
    /// </summary>
    /// <param name="bulletNumber"> 弾の番号 </param>
    /// <returns> 弾 </returns>
    public Bullet_BaseStatus GetTheBullet(string bulletNumber)
    {
        Bullet_BaseStatus bullet = null;

        StageScene_BulletData bulletData = stageScene_BulletData.FirstOrDefault(p => p.key == bulletNumber);

        /// 弾データ1
        StageScene_BulletAppearanceData appearanceData = stageScene_BulletAppearanceData.FirstOrDefault(p => p.key == bulletData.bulletAppearance);
        // 弾種類

        // スケール
        bullet.scale = appearanceData.bulletScale;

        /// 弾データ2
        StageScene_BulletBehaviorData bulletBehaviorData = stageScene_BulletBehaviorData.FirstOrDefault(p => p.key == bulletData.bulletBehavior);
        // 出現オフセット値
        bullet.spawnTimeOffset = bulletBehaviorData.spawnTimeOffset;
        // 予兆データ

        // スピード
        bullet.speed = bulletBehaviorData.bulletSpeed;

        /// 座標データ
        bullet.worldPosition = bulletData.worldPosition;

        /// 角度
        bullet.angle = bulletData.angle;

        /// 枠データ(今のところない)

        return bullet;
    }
}
