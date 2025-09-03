using TMPro;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

/// <summary>
/// ゲーム全体の状態管理
/// </summary>
public class GameManager : MonoBehaviour
{
    // 固定の数
    /// <summary> フレームレート </summary>
    private const int FrameRateSpeed = 60;

    // 変更可能な変数
    /// <summary>  </summary>

    /////////////////////////////////////////////////////
    ////////////////// クラス /////////////////////
    /// <summary> プレイヤークラス </summary>
    [SerializeField] private Player _player;
    /// <summary> プレイヤークラス </summary>
    [SerializeField] private StageGenerator _stageGenerator;
    /// <summary> 選択画面の管理 </summary>
    [SerializeField] private StageSelectSceneGenerator _stageSelectSceneGenerator;

    /// <summary>
    /// シーンの状態
    /// </summary>
    public enum SceneState
    {
        Title_Init,         // タイトルの初期化
        Title_Dsp,          // タイトルの周期化
        Title_End,          // タイトルの終期化
        StageSelect_Init,   // ステージ選択の初期化
        StageSelect_Dsp,    // ステージ選択の周期化
        StageSelect_End,    // ステージ選択の終期化
        Game_Init,          // ゲームの初期化
        Game_Dsp,           // ゲームの周期化
        Game_End,           // ゲームの終期化
        Result_Init,        // リザルトの初期化
        Result_Dsp,         // リザルトの初期化
        Result_End,         // リザルトの初期化
    }
    private SceneState _sceneState;

    /// <summary>
    /// シーンのオブジェクト
    /// </summary>
    public struct SceneObjects
    {
        public GameObject uiObj;    // UIに関するオブジェクト
        public GameObject gameObj;  // ゲームオブジェクト

        public SceneObjects(GameObject uiObj, GameObject gameObj)
        {
            this.uiObj = uiObj;
            this.gameObj = gameObj;
        }
    }
    private List<SceneObjects> _sceneObjects = new List<SceneObjects>();
    // タイトルのオブジェクト
    [SerializeField] private GameObject titleScene_uiObj;
    [SerializeField] private GameObject titleScene_gameObj;
    // ステージ選択のオブジェクト
    [SerializeField] private GameObject stageSelectScene_uiObj;
    [SerializeField] private GameObject stageSelectScene_gameObj;
    // ゲームのオブジェクト
    [SerializeField] private GameObject gameScene_uiObj;
    [SerializeField] private GameObject gameScene_gameObj;
    // リザルトのオブジェクト
    [SerializeField] private GameObject resultScene_uiObj;
    [SerializeField] private GameObject resultScene_gameObj;
    
    /// <summary> シーンの番号を保存 </summary>
    private int titleSceneNumber        = 0;
    private int stageSelectSceneNumber  = 0;
    private int gameSceneNumber         = 0;
    private int resultSceneNumber       = 0;

    /// <summary> ステージ番号 </summary>
    private int stageSelectNumber = -1;

    /// <summary>
    /// 初期化（一番目）
    /// </summary>
    private void Awake()
    {
        // フレームレートの初期化
        Application.targetFrameRate = FrameRateSpeed;

        // プレイヤーの初期化
        _player.InitSystem();
        _player.Init();

        // ステージの初期化
        _stageGenerator.InitSystem();
        _stageGenerator.Init();

        // ステージ選択の初期化
        _stageSelectSceneGenerator.InitSystem();
        _stageSelectSceneGenerator.Init();
    }

    /// <summary>
    /// 初期化（二番目）
    /// </summary>
    private void Start()
    {
        // GameManagerの初期化
        _sceneState = SceneState.Title_Init;

        // ゲームのオブジェクトを管理しやすくするために
        titleSceneNumber    = SetTheSceneObjects(titleScene_uiObj, titleScene_gameObj);
        stageSelectSceneNumber = SetTheSceneObjects(stageSelectScene_uiObj, stageSelectScene_gameObj);
        gameSceneNumber     = SetTheSceneObjects(gameScene_uiObj, gameScene_gameObj);
        resultSceneNumber   = SetTheSceneObjects(resultScene_uiObj, resultScene_gameObj);

        // ステージの壁の位置を代入してプレイヤーの範囲を決める
        Player.MovementRange movementRange;
        movementRange.up = 0;
        movementRange.down = 0;
        movementRange.left = 0;
        movementRange.right = 0;
        _stageGenerator.SetTheMovementRange(ref movementRange);
        _player.SetTheMovementRange(movementRange);

        // ステージ選択の初期設定
        _stageSelectSceneGenerator.SetTheGameManager(this);
    }

    /// <summary>
    /// フレームレート処理
    /// </summary>
    private void Update()
    {
        switch( _sceneState )
        {
            // タイトルのシーン
            case SceneState.Title_Init:
                SetObjectActive(titleSceneNumber);
                SetTheSceneState(SceneState.Title_Dsp);
                break;
            case SceneState.Title_Dsp:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    _sceneState = SceneState.Title_End;
                }
                break;
            case SceneState.Title_End:
                SetTheSceneState(SceneState.StageSelect_Init);
                break;

            // ステージ選択のシーン
            case SceneState.StageSelect_Init:
                stageSelectNumber = -1;
                SetObjectActive(stageSelectSceneNumber);
                SetTheSceneState(SceneState.StageSelect_Dsp);
                break;
            case SceneState.StageSelect_Dsp:
                if(stageSelectNumber >= 0)
                {
                    Debug.Log(stageSelectNumber);
                    SetTheSceneState(SceneState.StageSelect_End);
                }
                break;
            case SceneState.StageSelect_End:
                SetTheSceneState(SceneState.Game_Init);
                break;

            // ゲームシーン
            case SceneState.Game_Init:
                SetObjectActive(gameSceneNumber);
                SetTheSceneState(SceneState.Game_Dsp);
                break;
            case SceneState.Game_Dsp:
                break;
            case SceneState.Game_End:
                SetTheSceneState(SceneState.Result_Init);
                break;

            // リザルトシーン
            case SceneState.Result_Init:
                SetObjectActive(resultSceneNumber);
                SetTheSceneState(SceneState.Result_Dsp);
                break;
            case SceneState.Result_Dsp: 
                break;
            case SceneState.Result_End:
                SetTheSceneState(SceneState.Title_Init);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// フレームレート処理（移動関連）
    /// </summary>
    private void FixedUpdate()
    {

        switch (_sceneState)
        {
            case SceneState.Title_Init:
                break;
            case SceneState.Title_Dsp:
                break;
            case SceneState.Title_End:
                break;
            case SceneState.Game_Init:
                break;
            case SceneState.Game_Dsp:
                _player.Movement();
                break;
            case SceneState.Game_End:
                break;
            case SceneState.Result_Init:
                break;
            case SceneState.Result_Dsp:
                break;
            case SceneState.Result_End:
                break;
            default:
                break;
        }
    }

    /// <summary> シーンを設定する </summary>
    public void SetTheSceneState(SceneState sceneState)
    {
        _sceneState = sceneState;
    }
    /// <summary> シーンの状態を取得 </summary>
    public SceneState GetSceneState() { return _sceneState; }

    /// <summary> ステージ番号を設定する </summary>
    /// <param name="stageNumber"> ステージ番号 </param>
    public void SetTheStageNumber(int stageNumber)
    {
        stageSelectNumber = stageNumber;
    }

    //////////////////////////// シーンのオブジェクト管理 ////////////////////////////////
    /// <summary> シーンのオブジェクトを設定する </summary>
    /// <returns> リストの番号 </returns>
    public int SetTheSceneObjects(GameObject uiObj, GameObject gameObj)
    {
        int num = _sceneObjects.Count;
        _sceneObjects.Add(new SceneObjects(uiObj, gameObj));

        return num;
    }

    /// <summary> オブジェクトのアクティブ状態を変更する </summary>
    /// <remarks> 対応された番号によって true or false </remarks>
    /// <param name="sceneNumber"> シーンの番号 </param>
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
}
