using TMPro;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

/// <summary>
/// ƒQ[ƒ€‘S‘Ì‚Ìó‘ÔŠÇ—
/// </summary>
public class GameManager : MonoBehaviour
{
    // ŒÅ’è‚Ì”
    /// <summary> ƒtƒŒ[ƒ€ƒŒ[ƒg </summary>
    private const int FrameRateSpeed = 60;

<<<<<<< Updated upstream
    // •ÏX‰Â”\‚È•Ï”
    /// <summary>  </summary>

    /////////////////////////////////////////////////////
    ////////////////// ƒNƒ‰ƒX /////////////////////
    /// <summary> ƒvƒŒƒCƒ„[ƒNƒ‰ƒX </summary>
    [SerializeField] private Player _player;
    /// <summary> ƒQ[ƒ€ƒV[ƒ“‚ÌŠÇ— </summary>
=======
    /// <summary> ç¾åœ¨ã®æ™‚é–“ </summary>
    private int nowTime = 0;
    /// <summary> æ™‚é–“ã‚’ãƒªã‚»ãƒƒãƒˆã™ã‚‹ </summary>
    public void ResetTime() { nowTime = 0; }

    // å¤‰æ›´å¯èƒ½ãªå¤‰æ•°
    /// <summary>  </summary>

    /////////////////////////////////////////////////////
    ////////////////// ã‚¯ãƒ©ã‚¹ /////////////////////
    /// <summary> ã‚²ãƒ¼ãƒ ã‚·ãƒ¼ãƒ³ã®ç®¡ç† </summary>
>>>>>>> Stashed changes
    [SerializeField] private GameSceneManager _gameSceneManager;
    /// <summary> ‘I‘ğ‰æ–Ê‚ÌŠÇ— </summary>
    [SerializeField] private StageSelectSceneGenerator _stageSelectSceneGenerator;
    /// <summary> ƒQ[ƒ€ƒf[ƒ^‚ğŠÇ— </summary>
    private GameDataBase _gameDataBase;
    /// <summary> ƒƒCƒ“ƒf[ƒ^ </summary>
    [SerializeField] private MainData _mainData;

    /// <summary>
    /// ƒV[ƒ“‚Ìó‘Ô
    /// </summary>
    public enum SceneState
    {
        Title_Init,         // ƒ^ƒCƒgƒ‹‚Ì‰Šú‰»
        Title_Dsp,          // ƒ^ƒCƒgƒ‹‚ÌüŠú‰»
        Title_End,          // ƒ^ƒCƒgƒ‹‚ÌIŠú‰»
        StageSelect_Init,   // ƒXƒe[ƒW‘I‘ğ‚Ì‰Šú‰»
        StageSelect_Dsp,    // ƒXƒe[ƒW‘I‘ğ‚ÌüŠú‰»
        StageSelect_End,    // ƒXƒe[ƒW‘I‘ğ‚ÌIŠú‰»
        Game_Init,          // ƒQ[ƒ€‚Ì‰Šú‰»
        Game_Dsp,           // ƒQ[ƒ€‚ÌüŠú‰»
        Game_End,           // ƒQ[ƒ€‚ÌIŠú‰»
        Result_Init,        // ƒŠƒUƒ‹ƒg‚Ì‰Šú‰»
        Result_Dsp,         // ƒŠƒUƒ‹ƒg‚Ì‰Šú‰»
        Result_End,         // ƒŠƒUƒ‹ƒg‚Ì‰Šú‰»
    }
    private SceneState _sceneState;

    /// <summary>
    /// ã‚²ãƒ¼ãƒ ã®çµæœã®çŠ¶æ…‹
    /// </summary>
    public enum StageResultState
    {
        GameOver,
        GameClear,
    }
    private StageResultState stageResultState;

    /// <summary>
    /// ƒV[ƒ“‚ÌƒIƒuƒWƒFƒNƒg
    /// </summary>
    public struct SceneObjects
    {
        public GameObject uiObj;    // UI‚ÉŠÖ‚·‚éƒIƒuƒWƒFƒNƒg
        public GameObject gameObj;  // ƒQ[ƒ€ƒIƒuƒWƒFƒNƒg

        public SceneObjects(GameObject uiObj, GameObject gameObj)
        {
            this.uiObj = uiObj;
            this.gameObj = gameObj;
        }
    }
    private List<SceneObjects> _sceneObjects = new List<SceneObjects>();
    // ƒ^ƒCƒgƒ‹‚ÌƒIƒuƒWƒFƒNƒg
    [SerializeField] private GameObject titleScene_uiObj;
    [SerializeField] private GameObject titleScene_gameObj;
    // ƒXƒe[ƒW‘I‘ğ‚ÌƒIƒuƒWƒFƒNƒg
    [SerializeField] private GameObject stageSelectScene_uiObj;
    [SerializeField] private GameObject stageSelectScene_gameObj;
    // ƒQ[ƒ€‚ÌƒIƒuƒWƒFƒNƒg
    [SerializeField] private GameObject gameScene_uiObj;
    [SerializeField] private GameObject gameScene_gameObj;
    // ƒŠƒUƒ‹ƒg‚ÌƒIƒuƒWƒFƒNƒg
    [SerializeField] private GameObject resultScene_uiObj;
    [SerializeField] private GameObject resultScene_gameObj;
    
    /// <summary> ƒV[ƒ“‚Ì”Ô†‚ğ•Û‘¶ </summary>
    private int titleSceneNumber        = 0;
    private int stageSelectSceneNumber  = 0;
    private int gameSceneNumber         = 0;
    private int resultSceneNumber       = 0;

    /// <summary> ƒXƒe[ƒW”Ô† </summary>
    private int stageSelectNumber = -1;
    
    /// <summary>
    /// ã‚²ãƒ¼ãƒ ã‚·ãƒ¼ãƒ³ã®çŠ¶æ…‹
    /// </summary>
    public struct GameSceneStatus
    {
        // ã‚²ãƒ¼ãƒ ã‚·ãƒ¼ãƒ³ãŒçµ‚äº†ã—ã¦ã„ã‚‹ã‹
        public bool isGameSceneFinished;
        // ã‚²ãƒ¼ãƒ ã‚·ãƒ¼ãƒ³ãŒã‚²ãƒ¼ãƒ ã‚¯ãƒªã‚¢orã‚²ãƒ¼ãƒ ã‚ªãƒ¼ãƒãƒ¼ã«ãªã£ãŸæ™‚é–“
        public int gameFinishedTime;

<<<<<<< Updated upstream
    /// <summary> Œ»İ‚ÌŠÔ </summary>
    private int nowTime = 0;
    /// <summary> ŠÔ‚ğƒŠƒZƒbƒg‚·‚é </summary>
    public void ResetTime() { nowTime = 0; }
=======
        public GameSceneStatus(bool IsGameSceneFinished, int GameFinishedTime)
        {
            isGameSceneFinished = IsGameSceneFinished;
            gameFinishedTime = GameFinishedTime;
        }
    }
    private GameSceneStatus gameSceneStatus;
>>>>>>> Stashed changes

    /// <summary>
    /// ‰Šú‰»iˆê”Ô–Új
    /// </summary>
    private void Awake()
    {
        // ƒtƒŒ[ƒ€ƒŒ[ƒg‚Ì‰Šú‰»
        Application.targetFrameRate = FrameRateSpeed;

        // ƒƒCƒ“ƒf[ƒ^
        _mainData.MainDataInit();

        // ƒQ[ƒ€ƒf[ƒ^‚Ì‰Šú‰»
        _gameDataBase = new GameDataBase();
        _gameDataBase.LoadAll();

<<<<<<< Updated upstream
        // ƒvƒŒƒCƒ„[‚Ì‰Šú‰»
        _player.InitSystem();
        _player.Init();

        // ƒQ[ƒ€ƒV[ƒ“‚Ì‰Šú‰»
=======
        // ã‚²ãƒ¼ãƒ ã‚·ãƒ¼ãƒ³ã®åˆæœŸåŒ–
>>>>>>> Stashed changes
        _gameSceneManager.InitSystem();
        _gameSceneManager.Init();
        _gameSceneManager.SetTheGameManager(this);
        _gameSceneManager.SetTheMainData(_mainData);

        // ƒXƒe[ƒW‘I‘ğ‚Ì‰Šú‰»
        _stageSelectSceneGenerator.InitSystem();
        _stageSelectSceneGenerator.Init();
    }

    /// <summary>
    /// ‰Šú‰»i“ñ”Ô–Új
    /// </summary>
    private void Start()
    {
        // GameManager‚Ì‰Šú‰»
        _sceneState = SceneState.Title_Init;

<<<<<<< Updated upstream
        // ƒQ[ƒ€‚ÌƒIƒuƒWƒFƒNƒg‚ğŠÇ—‚µ‚â‚·‚­‚·‚é‚½‚ß‚É
=======
        stageResultState = StageResultState.GameOver;

        // ã‚²ãƒ¼ãƒ ã®ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆã‚’ç®¡ç†ã—ã‚„ã™ãã™ã‚‹ãŸã‚ã«
>>>>>>> Stashed changes
        titleSceneNumber    = SetTheSceneObjects(titleScene_uiObj, titleScene_gameObj);
        stageSelectSceneNumber = SetTheSceneObjects(stageSelectScene_uiObj, stageSelectScene_gameObj);
        gameSceneNumber     = SetTheSceneObjects(gameScene_uiObj, gameScene_gameObj);
        resultSceneNumber   = SetTheSceneObjects(resultScene_uiObj, resultScene_gameObj);

        // ƒXƒe[ƒW‚Ì•Ç‚ÌˆÊ’u‚ğ‘ã“ü‚µ‚ÄƒvƒŒƒCƒ„[‚Ì”ÍˆÍ‚ğŒˆ‚ß‚é
        Player.MovementRange movementRange = new Player.MovementRange();
        _gameSceneManager.SetTheMovementRange(ref movementRange);
        _gameSceneManager.SetTheMovementRange(movementRange);

        // ƒXƒe[ƒW‘I‘ğ‚Ì‰Šúİ’è
        _stageSelectSceneGenerator.SetTheGameManager(this);
    }

    /// <summary>
    /// ƒtƒŒ[ƒ€ƒŒ[ƒgˆ—
    /// </summary>
    private void Update()
    {
        nowTime++;
        switch( _sceneState )
        {
            // ƒ^ƒCƒgƒ‹‚ÌƒV[ƒ“
            case SceneState.Title_Init:
                ResetTime();
                SetObjectActive(titleSceneNumber);
                SetTheSceneState(SceneState.Title_Dsp);
                break;
            case SceneState.Title_Dsp:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    SetTheSceneState(SceneState.Title_End);
                }
                break;
            case SceneState.Title_End:
                SetTheSceneState(SceneState.StageSelect_Init);
                break;

            // ƒXƒe[ƒW‘I‘ğ‚ÌƒV[ƒ“
            case SceneState.StageSelect_Init:
                stageSelectNumber = -1;
                ResetTime();
                SetObjectActive(stageSelectSceneNumber);
                SetTheSceneState(SceneState.StageSelect_Dsp);
                break;
            case SceneState.StageSelect_Dsp:
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    SetTheSceneState(SceneState.Title_Init);
                }

                if(stageSelectNumber >= 0)
                {
                    Debug.Log(stageSelectNumber);
                    SetTheSceneState(SceneState.StageSelect_End);
                }
                break;
            case SceneState.StageSelect_End:
                SetTheSceneState(SceneState.Game_Init);
                break;

            // ƒQ[ƒ€ƒV[ƒ“
            case SceneState.Game_Init:
                ResetTime();
                SetObjectActive(gameSceneNumber);

                // ƒXƒe[ƒW‚Ì‰Šú‰»
                _gameSceneManager.SetTheStageNumber(stageSelectNumber);
                _gameSceneManager.GameSceneInit();

                gameSceneStatus = new GameSceneStatus(false, -1);

                SetTheSceneState(SceneState.Game_Dsp);

                _gameSceneManager.SetTheGameFinish(() => TriggerGameOverEvent(), () => TriggerGameClearEvent());
                
                break;
            case SceneState.Game_Dsp:
                _gameSceneManager.GameSceneDPS(1);
                
                //if (Input.GetKeyDown(KeyCode.Space))
                //{
                //    SetTheSceneState(SceneState.Game_End);
                //}

                if( gameSceneStatus.isGameSceneFinished && gameSceneStatus.gameFinishedTime >= 0)
                {
                    if( nowTime >= (gameSceneStatus.gameFinishedTime + (FrameRateSpeed * 0)))
                    {
                        SetTheSceneState(SceneState.Game_End);
                    }
                }

                //if(nowTime >= FrameRateSpeed * 10)
                //{
                //    SetTheSceneState(SceneState.Game_End);
                //}
                break;
            case SceneState.Game_End:
                SetTheSceneState(SceneState.Result_Init);
                _gameSceneManager.GameScene_End();
                break;

            // ƒŠƒUƒ‹ƒgƒV[ƒ“
            case SceneState.Result_Init:
                ResetTime();
                SetObjectActive(resultSceneNumber);
                SetTheSceneState(SceneState.Result_Dsp);

                switch (stageResultState)
                {
                    case StageResultState.GameOver:
                        Debug.Log("GameOver");
                        break;
                    case StageResultState.GameClear:
                        Debug.Log("GameClear");
                        break;
                }
                break;
            case SceneState.Result_Dsp:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SetTheSceneState(SceneState.Result_End);
                }
                break;
            case SceneState.Result_End:
                SetTheSceneState(SceneState.StageSelect_Init);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ƒtƒŒ[ƒ€ƒŒ[ƒgˆ—iˆÚ“®ŠÖ˜Aj
    /// </summary>
    private void FixedUpdate()
    {

        switch (_sceneState)
        {
            case SceneState.Title_Dsp:
                break;
            case SceneState.Game_Dsp:
                _gameSceneManager.GameSceneDeltaDPS(nowTime);
                break;
            case SceneState.Result_Dsp:
                break;
            default:
                break;
        }
    }

    /// <summary> ƒV[ƒ“‚ğİ’è‚·‚é </summary>
    public void SetTheSceneState(SceneState sceneState)
    {
        _sceneState = sceneState;
    }
    /// <summary> ƒV[ƒ“‚Ìó‘Ô‚ğæ“¾ </summary>
    public SceneState GetSceneState() { return _sceneState; }

    /// <summary> ƒXƒe[ƒW”Ô†‚ğİ’è‚·‚é </summary>
    /// <param name="stageNumber"> ƒXƒe[ƒW”Ô† </param>
    public void SetTheStageNumber(int stageNumber)
    {
        stageSelectNumber = stageNumber;
    }

    //////////////////////////// ƒV[ƒ“‚ÌƒIƒuƒWƒFƒNƒgŠÇ— ////////////////////////////////
    /// <summary> ƒV[ƒ“‚ÌƒIƒuƒWƒFƒNƒg‚ğİ’è‚·‚é </summary>
    /// <returns> ƒŠƒXƒg‚Ì”Ô† </returns>
    public int SetTheSceneObjects(GameObject uiObj, GameObject gameObj)
    {
        int num = _sceneObjects.Count;
        _sceneObjects.Add(new SceneObjects(uiObj, gameObj));

        return num;
    }

    /// <summary> ƒIƒuƒWƒFƒNƒg‚ÌƒAƒNƒeƒBƒuó‘Ô‚ğ•ÏX‚·‚é </summary>
    /// <remarks> ‘Î‰‚³‚ê‚½”Ô†‚É‚æ‚Á‚Ä true or false </remarks>
    /// <param name="sceneNumber"> ƒV[ƒ“‚Ì”Ô† </param>
    public void SetObjectActive(int sceneNumber)
    {
        int number = 0;

        foreach(SceneObjects sceneObjects in _sceneObjects)
        {
            bool isActive = (sceneNumber == number);
            sceneObjects.uiObj.SetActive(isActive);
            sceneObjects.gameObj.SetActive(isActive);
            number++;
        }
    }

    /// <summary> ƒQ[ƒ€ƒf[ƒ^‚Ìî•ñ‚ğ•Ô‚· </summary>
    public GameDataBase GetTheGameDataBase(){ return _gameDataBase;}

    /// <summary> ã‚²ãƒ¼ãƒ ã‚¯ãƒªã‚¢ã®ã‚¤ãƒ™ãƒ³ãƒˆã‚’ç™ºç«ã•ã›ã‚‹ </summary>
    public void TriggerGameClearEvent()
    {
        SetCurrentGameSceneStatus();
        stageResultState = StageResultState.GameClear;
    }

    /// <summary> ã‚²ãƒ¼ãƒ ã‚ªãƒ¼ãƒãƒ¼ã®ã‚¤ãƒ™ãƒ³ãƒˆã‚’ç™ºç«ã•ã›ã‚‹ </summary>
    public void TriggerGameOverEvent()
    {
        stageResultState = StageResultState.GameOver;
        SetCurrentGameSceneStatus();
    }

    /// <summary>
    /// ç¾åœ¨ã®ã‚·ãƒ¼ãƒ³çŠ¶æ…‹ã‚’å–å¾—ã™ã‚‹
    /// </summary>
    public void SetCurrentGameSceneStatus()
    {
        gameSceneStatus.isGameSceneFinished = true;
        gameSceneStatus.gameFinishedTime = nowTime;
    }
}