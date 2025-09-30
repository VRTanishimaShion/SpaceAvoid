using NUnit.Framework;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// ゲームデータを管理
/// </summary>
public class GameDataBase
{
    /// <summary> ステージデータの管理 </summary>
    public List<Queue<StageData>> stagesData { get; private set; } = new List<Queue<StageData>>();

    /// <summary>
    /// 全てのゲームデータを読み込む
    /// </summary>
    public void LoadAll()
    {
        LoadStage();
    }

    /// <summary>
    /// ステージの読み込み
    /// </summary>
    public void LoadStage()
    {
        int stageNumber = 1;

        while (File.Exists(Application.streamingAssetsPath + "/Stage" + stageNumber.ToString() + ".csv"))
        {
            List<string[]> stageDataCsv = CSVLoader.Load("/Stage" + stageNumber.ToString() + ".csv");
            var stage = new Queue<StageData>();

            foreach (string[] bullet in stageDataCsv)
            {
                stage.Enqueue(new StageData(bullet));
            }

            stagesData.Add(stage);

            stageNumber++;
        }
    }
}
