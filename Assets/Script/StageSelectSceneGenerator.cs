using UnityEngine;

/// <summary>
/// �I����ʂ̊Ǘ�
/// </summary>
public class StageSelectSceneGenerator : MonoBehaviour
{
    /// <summary> �Q�[���}�l�[�W���[�̃N���X </summary>
    [SerializeField] private StageButton _stageButton;
    /// <summary> �Q�[���}�l�[�W���[�̃N���X </summary>
    private GameManager _gameManager;

    /// <summary> Unity�̋@�\�̏��� </summary>
    public void InitSystem()
    {

    }
    /// <summary> �ϐ��̏������Ȃ� </summary>
    public void Init()
    {

    }

    /// <summary> �Q�[���}�l�[�W���[�̃N���X��ݒ肷�� </summary>
    /// <param name="gameManager"> �Q�[���}�l�[�W���[�̃N���X </param>
    public void SetTheGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
        _stageButton.SetTheGameManager(_gameManager);
    }
}
