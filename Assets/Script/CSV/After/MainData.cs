using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// csvの全ての読み込み場所
/// </summary>
public class MainData
{
    //////////  ステージシーン  //////////
    /// <summary> ステージシーン </summary>
    private List<StageScene> stageSceneData = new List<StageScene>();
    /// <summary> フェーズデータ </summary>
    private List<StageScene_PhaseData> stageScene_PhasesData = new List<StageScene_PhaseData>();
    /// <summary> 弾のデータ </summary>
    private List<StageScene_BulletData> stageScene_BulletData = new List<StageScene_BulletData>();
    /// <summary> 弾の形のデータ </summary>
    private List<StageScene_BulletAppearanceData> stageScene_BulletAppearanceData = new List<StageScene_BulletAppearanceData>();
    /// <summary> 弾の挙動のデータ </summary>
    private List<StageScene_BulletBehaviorData> stageScene_BulletBehaviorDatas = new List<StageScene_BulletBehaviorData>();
    /// <summary> 弾の挙動のデータ </summary>
    private List<StageScene_BulletPreEffectData> stageScene_BulletPreEffectDatas = new List<StageScene_BulletPreEffectData>();



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
        // ステージデータ
        string[][] stageData = await CSVReader.LoadCSVData("StageData.csv");

        int nowPhasesNumber = 0;
        string baseName = "PhsDt_";
        if (stageData != null)
        {
            for (int i = 2; i < stageData.GetLength(0); i++)
            {
                List<string> phases = new List<string>();
                string phaseName = stageData[i][1];
                char phaseNumber = phaseName[^1];

                for( ; nowPhasesNumber <= (int)(phaseNumber - '0'); nowPhasesNumber++)
                {
                    phases.Add((baseName) + (nowPhasesNumber.ToString("D2")));
                }

                stageSceneData.Add(new StageScene(stageData[i][0], phases));
            }
        }
        else
        {
            Debug.LogError("ステージデータの情報が取れていない");
        }

        // フェーズデータ
        string[][] phasesData = await CSVReader.LoadCSVData("");
        if (phasesData != null)
        {
            for(int i = 1; i < phasesData.GetLength(0);i++)
            {
                List<string> phase_strings = new List<string>();
                phase_strings = phasesData[i].ToList<string>();
                phase_strings.Remove(phasesData[i][0]);

                stageScene_PhasesData.Add(new StageScene_PhaseData(phasesData[i][0], phase_strings));
            }
        }
        else
        {
            Debug.LogError("フェーズデータの情報が取れていない");
        }

        // 弾のデータ
        string[][] bulletData = await CSVReader.LoadCSVData("");
        if(bulletData != null)
        {
            for(int i = 1; i < bulletData.GetLength(0); i++)
            {
                stageScene_BulletData.Add(
                    new StageScene_BulletData
                    (
                    bulletData[i][0],
                    bulletData[i][1],
                    bulletData[i][2],
                    bulletData[i][3],
                    bulletData[i][4],
                    bulletData[i][5]
                    )
                );
            }
        }
        else
        {
            Debug.LogError("弾のデータの情報が取れていない");
        }

        // 弾の種類のデータ
        string[][] bulletAppearanceData = await CSVReader.LoadCSVData("");
        if(bulletAppearanceData != null)
        {
            for(int i = 1; i < bulletAppearanceData.GetLength(0);i++)
            {
                stageScene_BulletAppearanceData.Add(
                    new StageScene_BulletAppearanceData(
                        bulletAppearanceData[i][0],
                        bulletAppearanceData[i][1]
                    )
                );
            }
        }
        else
        {
            Debug.LogError("弾の種類のデータの情報が取れていない");
        }

        // 弾の挙動を表すデータ
        string[][] bulletBehaviorData = await CSVReader.LoadCSVData("");
        if(bulletBehaviorData != null)
        {
            for(int i = 1; i < bulletBehaviorData.GetLength(0); i++)
            {
                stageScene_BulletBehaviorDatas.Add(
                    new StageScene_BulletBehaviorData(
                        bulletBehaviorData[i][0],
                        bulletBehaviorData[i][1],
                        float.Parse(bulletBehaviorData[i][2])
                    )
                );
                
            }
        }
        else
        {
            Debug.LogError("弾の挙動を表すデータの情報が取れていない");
        }
    }
}
