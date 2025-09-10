using NUnit.Framework;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// �Q�[���f�[�^���Ǘ�
/// </summary>
public class GameDataBase
{
    /// <summary> �X�e�[�W�f�[�^�̊Ǘ� </summary>
    public List<Queue<StageData>> stagesData { get; private set; } = new List<Queue<StageData>>();

    /// <summary>
    /// �S�ẴQ�[���f�[�^��ǂݍ���
    /// </summary>
    public void LoadAll()
    {
        LoadStage();
    }

    /// <summary>
    /// �X�e�[�W�̓ǂݍ���
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
