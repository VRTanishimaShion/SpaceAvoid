using TMPro;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

/// <summary>
/// �Q�[���S�̂̏�ԊǗ�
/// </summary>
public class GameManager : MonoBehaviour
{
    // �Œ�̐�
    /// <summary> �t���[�����[�g </summary>
    private const int FrameRateSpeed = 60;

    // �ύX�\�ȕϐ�
    /// <summary>  </summary>

    /////////////////////////////////////////////////////
    ////////////////// �N���X /////////////////////
    /// <summary> �v���C���[�N���X </summary>
    [SerializeField] private Player _player;
    /// <summary> �v���C���[�N���X </summary>
    [SerializeField] private StageGenerator _stageGenerator;
    /// <summary> �I����ʂ̊Ǘ� </summary>
    [SerializeField] private StageSelectSceneGenerator _stageSelectSceneGenerator;

    /// <summary>
    /// �V�[���̏��
    /// </summary>
    public enum SceneState
    {
        Title_Init,         // �^�C�g���̏�����
        Title_Dsp,          // �^�C�g���̎�����
        Title_End,          // �^�C�g���̏I����
        StageSelect_Init,   // �X�e�[�W�I���̏�����
        StageSelect_Dsp,    // �X�e�[�W�I���̎�����
        StageSelect_End,    // �X�e�[�W�I���̏I����
        Game_Init,          // �Q�[���̏�����
        Game_Dsp,           // �Q�[���̎�����
        Game_End,           // �Q�[���̏I����
        Result_Init,        // ���U���g�̏�����
        Result_Dsp,         // ���U���g�̏�����
        Result_End,         // ���U���g�̏�����
    }
    private SceneState _sceneState;

    /// <summary>
    /// �V�[���̃I�u�W�F�N�g
    /// </summary>
    public struct SceneObjects
    {
        public GameObject uiObj;    // UI�Ɋւ���I�u�W�F�N�g
        public GameObject gameObj;  // �Q�[���I�u�W�F�N�g

        public SceneObjects(GameObject uiObj, GameObject gameObj)
        {
            this.uiObj = uiObj;
            this.gameObj = gameObj;
        }
    }
    private List<SceneObjects> _sceneObjects = new List<SceneObjects>();
    // �^�C�g���̃I�u�W�F�N�g
    [SerializeField] private GameObject titleScene_uiObj;
    [SerializeField] private GameObject titleScene_gameObj;
    // �X�e�[�W�I���̃I�u�W�F�N�g
    [SerializeField] private GameObject stageSelectScene_uiObj;
    [SerializeField] private GameObject stageSelectScene_gameObj;
    // �Q�[���̃I�u�W�F�N�g
    [SerializeField] private GameObject gameScene_uiObj;
    [SerializeField] private GameObject gameScene_gameObj;
    // ���U���g�̃I�u�W�F�N�g
    [SerializeField] private GameObject resultScene_uiObj;
    [SerializeField] private GameObject resultScene_gameObj;
    
    /// <summary> �V�[���̔ԍ���ۑ� </summary>
    private int titleSceneNumber        = 0;
    private int stageSelectSceneNumber  = 0;
    private int gameSceneNumber         = 0;
    private int resultSceneNumber       = 0;

    /// <summary> �X�e�[�W�ԍ� </summary>
    private int stageSelectNumber = -1;

    /// <summary>
    /// �������i��Ԗځj
    /// </summary>
    private void Awake()
    {
        // �t���[�����[�g�̏�����
        Application.targetFrameRate = FrameRateSpeed;

        // �v���C���[�̏�����
        _player.InitSystem();
        _player.Init();

        // �X�e�[�W�̏�����
        _stageGenerator.InitSystem();
        _stageGenerator.Init();

        // �X�e�[�W�I���̏�����
        _stageSelectSceneGenerator.InitSystem();
        _stageSelectSceneGenerator.Init();
    }

    /// <summary>
    /// �������i��Ԗځj
    /// </summary>
    private void Start()
    {
        // GameManager�̏�����
        _sceneState = SceneState.Title_Init;

        // �Q�[���̃I�u�W�F�N�g���Ǘ����₷�����邽�߂�
        titleSceneNumber    = SetTheSceneObjects(titleScene_uiObj, titleScene_gameObj);
        stageSelectSceneNumber = SetTheSceneObjects(stageSelectScene_uiObj, stageSelectScene_gameObj);
        gameSceneNumber     = SetTheSceneObjects(gameScene_uiObj, gameScene_gameObj);
        resultSceneNumber   = SetTheSceneObjects(resultScene_uiObj, resultScene_gameObj);

        // �X�e�[�W�̕ǂ̈ʒu�������ăv���C���[�͈̔͂����߂�
        Player.MovementRange movementRange;
        movementRange.up = 0;
        movementRange.down = 0;
        movementRange.left = 0;
        movementRange.right = 0;
        _stageGenerator.SetTheMovementRange(ref movementRange);
        _player.SetTheMovementRange(movementRange);

        // �X�e�[�W�I���̏����ݒ�
        _stageSelectSceneGenerator.SetTheGameManager(this);
    }

    /// <summary>
    /// �t���[�����[�g����
    /// </summary>
    private void Update()
    {
        switch( _sceneState )
        {
            // �^�C�g���̃V�[��
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

            // �X�e�[�W�I���̃V�[��
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

            // �Q�[���V�[��
            case SceneState.Game_Init:
                SetObjectActive(gameSceneNumber);
                SetTheSceneState(SceneState.Game_Dsp);
                break;
            case SceneState.Game_Dsp:
                break;
            case SceneState.Game_End:
                SetTheSceneState(SceneState.Result_Init);
                break;

            // ���U���g�V�[��
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
    /// �t���[�����[�g�����i�ړ��֘A�j
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

    /// <summary> �V�[����ݒ肷�� </summary>
    public void SetTheSceneState(SceneState sceneState)
    {
        _sceneState = sceneState;
    }
    /// <summary> �V�[���̏�Ԃ��擾 </summary>
    public SceneState GetSceneState() { return _sceneState; }

    /// <summary> �X�e�[�W�ԍ���ݒ肷�� </summary>
    /// <param name="stageNumber"> �X�e�[�W�ԍ� </param>
    public void SetTheStageNumber(int stageNumber)
    {
        stageSelectNumber = stageNumber;
    }

    //////////////////////////// �V�[���̃I�u�W�F�N�g�Ǘ� ////////////////////////////////
    /// <summary> �V�[���̃I�u�W�F�N�g��ݒ肷�� </summary>
    /// <returns> ���X�g�̔ԍ� </returns>
    public int SetTheSceneObjects(GameObject uiObj, GameObject gameObj)
    {
        int num = _sceneObjects.Count;
        _sceneObjects.Add(new SceneObjects(uiObj, gameObj));

        return num;
    }

    /// <summary> �I�u�W�F�N�g�̃A�N�e�B�u��Ԃ�ύX���� </summary>
    /// <remarks> �Ή����ꂽ�ԍ��ɂ���� true or false </remarks>
    /// <param name="sceneNumber"> �V�[���̔ԍ� </param>
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
