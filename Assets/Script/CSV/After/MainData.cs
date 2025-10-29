using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// csv‚Ì‘S‚Ä‚Ì“Ç‚İ‚İêŠ
/// </summary>
public class MainData : MonoBehaviour
{
    //////////  ƒXƒe[ƒWƒV[ƒ“  //////////
    /// <summary> ‹¤—Lƒf[ƒ^ </summary>
    private List<StageScene_SharedData> stageScene_SharedData = new List<StageScene_SharedData>();
    /// <summary> ƒXƒe[ƒWƒV[ƒ“ </summary>
    private List<StageScene> stageSceneData = new List<StageScene>();
    /// <summary> ƒtƒF[ƒYƒf[ƒ^ </summary>
    private List<StageScene_PhaseData> stageScene_PhasesData = new List<StageScene_PhaseData>();
    /// <summary> ’e‚Ìƒf[ƒ^ </summary>
    private List<StageScene_BulletData> stageScene_BulletData = new List<StageScene_BulletData>();
    /// <summary> ’e‚ÌŒ`‚Ìƒf[ƒ^ </summary>
    private List<StageScene_BulletAppearanceData> stageScene_BulletAppearanceData = new List<StageScene_BulletAppearanceData>();
    /// <summary> ’e‚Ì‹““®‚Ìƒf[ƒ^ </summary>
    private List<StageScene_BulletBehaviorData> stageScene_BulletBehaviorData = new List<StageScene_BulletBehaviorData>();
    /// <summary> ’e‚Ì‹““®‚Ìƒf[ƒ^ </summary>
    private List<StageScene_BulletPreEffectData> stageScene_BulletPreEffectData = new List<StageScene_BulletPreEffectData>();

    /// <summary>
    /// ‰Šú‰»
    /// </summary>
    public void MainDataInit()
    {
        _ = ReadStageSceneData();
    }

    /// <summary>
    /// ƒXƒe[ƒWƒV[ƒ“
    /// </summary>
    private async Task ReadStageSceneData()
    {
        // ‹¤—Lƒf[ƒ^
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
            Debug.Log("‹¤—Lƒf[ƒ^‚Ìî•ñ‚ªæ‚ê‚Ä‚¢‚È‚¢");
        }

        // ƒXƒe[ƒWƒf[ƒ^
        string[][] stageData = await CSVReader.LoadCSVData("StageData.csv");

        string baseName = "PhsDt_";
        if (stageData != null)
        {
            for (int i = 1; i < stageData.GetLength(0); i++)
            {
                List<string> phases = new List<string>();

                // é–‹å§‹ãƒ•ã‚§ãƒ¼ã‚º
                string phaseName1 = stageData[i][1];
                string numberPart1 = phaseName1.Split('_')[1];
                int nowPhasesNumber = int.Parse(numberPart1);

                // æœ€çµ‚ãƒ•ã‚§ãƒ¼ã‚º
                string phaseName2 = stageData[i][2];
                string numberPart2 = phaseName2.Split('_')[1];
                int phaseNumber2 = int.Parse(numberPart2);

                for( ; nowPhasesNumber <= phaseNumber2; nowPhasesNumber++)
                {
                    phases.Add((baseName) + (nowPhasesNumber.ToString("D2")));
                }
<<<<<<< Updated upstream
=======
                
                stageSceneData.Add(new StageScene(stageData[i][0], phases, int.Parse(stageData[i][3])));
>>>>>>> Stashed changes

                stageSceneData.Add(new StageScene(stageData[i][0], phases, int.Parse(stageData[i][2])));
            }
        }
        else
        {
            Debug.LogError("ƒXƒe[ƒWƒf[ƒ^‚Ìî•ñ‚ªæ‚ê‚Ä‚¢‚È‚¢");
        }

        // ƒtƒF[ƒYƒf[ƒ^
        string[][] phasesData = await CSVReader.LoadCSVData("PhaseData.csv");
        if (phasesData != null)
        {
            for(int i = 3; i < phasesData.GetLength(0);i++)
            {
                List<string> bullets = new List<string>();
                bullets = phasesData[i].ToList<string>();
                bullets.Remove(phasesData[i][0]);

                stageScene_PhasesData.Add(new StageScene_PhaseData(phasesData[i][0], bullets));
            }
        }
        else
        {
            Debug.LogError("ƒtƒF[ƒYƒf[ƒ^‚Ìî•ñ‚ªæ‚ê‚Ä‚¢‚È‚¢");
        }

        // ’e‚Ìƒf[ƒ^
        string[][] bulletData = await CSVReader.LoadCSVData("BulletData.csv");
        if(bulletData != null)
        {
            for(int i = 1; i < bulletData.GetLength(0); i++)
            {
                float positionX = 0, positionY = 0;
                Vector2 angle = Vector2.zero;

                ///ƒVƒ“ƒ{ƒ‹’è‹`‚³‚ê‚Ä‚¢‚é‚©ŒŸõ‚·‚é
                // ˆÊ’u
                StageScene_SharedData x = stageScene_SharedData.FirstOrDefault(p => p.key == bulletData[i][3]);
                StageScene_SharedData y = stageScene_SharedData.FirstOrDefault(p => p.key == bulletData[i][4]);

                positionX = (x != null) ? x.parameter : float.Parse(bulletData[i][3]);
                positionY = (y != null) ? y.parameter : float.Parse(bulletData[i][4]);

                float degrees = float.Parse(bulletData[i][5]);
                float radians = degrees * Mathf.Deg2Rad;

                // Šp“x
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
            Debug.LogError("’e‚Ìƒf[ƒ^‚Ìî•ñ‚ªæ‚ê‚Ä‚¢‚È‚¢");
        }

        // ’e‚Ìí—Ş‚Ìƒf[ƒ^(’eƒf[ƒ^‡@)
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
            Debug.LogError("’e‚Ìí—Ş‚Ìƒf[ƒ^‚Ìî•ñ‚ªæ‚ê‚Ä‚¢‚È‚¢");
        }

        // ’e‚Ì‹““®‚ğ•\‚·ƒf[ƒ^(’eƒf[ƒ^‡A)
        string[][] bulletBehaviorData = await CSVReader.LoadCSVData("BltAttDt_2.csv");
        if(bulletBehaviorData != null)
        {
            for(int i = 1; i < bulletBehaviorData.GetLength(0); i++)
            {
                float bulletSpeed = 0;

                ///ƒVƒ“ƒ{ƒ‹’è‹`‚³‚ê‚Ä‚¢‚é‚©ŒŸõ‚·‚é
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
            Debug.LogError("’e‚Ì‹““®‚ğ•\‚·ƒf[ƒ^‚Ìî•ñ‚ªæ‚ê‚Ä‚¢‚È‚¢");
        }


        // å¼¾ã®äºˆå…†ãƒ‡ãƒ¼ã‚¿
        string[][] PredictiveData = await CSVReader.LoadCSVData("PredictiveData.csv");
        if(PredictiveData != null)
        {
            for(int i = 2; i < PredictiveData.GetLength(0); i++)
            {
                List<float> effectData = new List<float>();
                List<string> data = new List<string>();
                data = PredictiveData[i].ToList<string>();
                data.Remove(PredictiveData[i][0]);

                foreach(string s in data)
                {
                    effectData.Add(float.Parse(s));
                }

                stageScene_BulletPreEffectData.Add(
                    new StageScene_BulletPreEffectData(
                        PredictiveData[i][0],
                        effectData
                    )
                );
            }
        }
        else
        {
            Debug.LogError("å¼¾ã®äºˆå…†ã§ãƒ‡ãƒ¼ã‚¿ã®æƒ…å ±ãŒå–ã‚Œã¦ã„ãªã„");
        }
    }

    /// <summary>
    /// ‘I‘ğ‚³‚ê‚½ƒXƒe[ƒW”Ô†‚ÅƒtƒF[ƒY‚ğ•Ô‚·
    /// </summary>
    /// <param name="selectStageNumber"> ƒXƒe[ƒW”Ô† </param>
    /// <returns> ƒtƒF[ƒY”Ô† </returns>
    public List<string> GetThePhases(int selectStageNumber)
    {
        return stageSceneData[selectStageNumber].phase;
    }

    /// <summary>
    /// ƒtƒF[ƒY“à‚Ì’eƒf[ƒ^(string)‚ğæ“¾
    /// </summary>
    /// <param name="phaseNumber"> ƒtƒF[ƒY”Ô† </param>
    /// <returns> 
    /// ¬Œ÷ : ’eƒf[ƒ^‚ğæ“¾‚Å‚«‚é
    /// ¸”s : null
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
<<<<<<< Updated upstream
            Debug.Log("ƒtƒF[ƒYƒf[ƒ^“à‚ÌƒoƒŒƒbƒg‚Ì”Ô†(string)‚ªó‚¯æ‚ê‚È‚¢");
=======
            //Debug.Log("ãƒ•ã‚§ãƒ¼ã‚ºãƒ‡ãƒ¼ã‚¿å†…ã®ãƒãƒ¬ãƒƒãƒˆã®ç•ªå·(string)ãŒå—ã‘å–ã‚Œãªã„");
>>>>>>> Stashed changes
        }

        return null;
    }

    /// <summary>
    /// ’e‚ğæ“¾‚·‚é
    /// </summary>
    /// <param name="bulletNumber"> ’e‚Ì”Ô† </param>
    /// <returns> ’e </returns>
    public Bullet_BaseStatus GetTheBullet(string bulletNumber)
    {
        Bullet_BaseStatus bullet;

        StageScene_BulletData bulletData = stageScene_BulletData.FirstOrDefault(p => p.key == bulletNumber);

        /// ’eƒf[ƒ^1
        StageScene_BulletAppearanceData appearanceData = stageScene_BulletAppearanceData.FirstOrDefault(p => p.key == bulletData.bulletAppearance);
        // ’eí—Ş

<<<<<<< Updated upstream
        // ƒXƒP[ƒ‹
        bullet.scale = appearanceData.bulletScale;
=======
        // ã‚¹ã‚±ãƒ¼ãƒ«
        Vector2 scale = appearanceData.bulletScale;
>>>>>>> Stashed changes

        /// ’eƒf[ƒ^2
        StageScene_BulletBehaviorData bulletBehaviorData = stageScene_BulletBehaviorData.FirstOrDefault(p => p.key == bulletData.bulletBehavior);
<<<<<<< Updated upstream
        // oŒ»ƒIƒtƒZƒbƒg’l
        bullet.spawnTimeOffset = bulletBehaviorData.spawnTimeOffset;
        // —\’›ƒf[ƒ^

        // ƒXƒs[ƒh
        bullet.speed = bulletBehaviorData.bulletSpeed;

        /// À•Wƒf[ƒ^
        bullet.worldPosition = bulletData.worldPosition;

        /// Šp“x
        bullet.angle = bulletData.angle;
=======
        // å‡ºç¾ã‚ªãƒ•ã‚»ãƒƒãƒˆå€¤
        float spawnTimeOffset = bulletBehaviorData.spawnTimeOffset;
        // äºˆå…†ãƒ‡ãƒ¼ã‚¿
        List<float> preEffect = stageScene_BulletPreEffectData.FirstOrDefault(p => p.key == bulletBehaviorData.bulletPreEffect).GetTheEffectTime();

        // ã‚¹ãƒ”ãƒ¼ãƒ‰
        float speed = bulletBehaviorData.bulletSpeed;

        /// åº§æ¨™ãƒ‡ãƒ¼ã‚¿
        Vector2 worldPosition = bulletData.worldPosition;

        /// è§’åº¦
        Vector2 angle = bulletData.angle;

        // æ 
        Player.MovementRange range = new Player.MovementRange();
>>>>>>> Stashed changes

        /// ˜gƒf[ƒ^(¡‚Ì‚Æ‚±‚ë‚È‚¢)

        bullet = new Bullet_BaseStatus(
            null, scale, spawnTimeOffset, preEffect, speed, worldPosition, angle, range
            );
        return bullet;
    }
}
