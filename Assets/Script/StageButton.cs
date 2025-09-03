using UnityEngine;

/// <summary>
/// �X�e�[�W�̃{�^��
/// </summary>
public class StageButton : MonoBehaviour
{
    /// <summary> �Q�[���}�l�[�W���[�̃N���X </summary>
    private GameManager _gameManager;

    /// <summary>
    /// �X�e�[�W�{�^���������ꂽ�Ƃ�
    /// </summary>
    /// <param name="stageNumber"> �X�e�[�W�ԍ� </param>
    public void OnStageButton(int stageNumber)
    {
        if(stageNumber < 0)
        {
            Debug.LogError("���݂��Ȃ��ԍ�");
        }
        _gameManager.SetTheStageNumber(stageNumber);
    }

    /// <summary> �Q�[���}�l�[�W���[�̃N���X��ݒ肷�� </summary>
    /// <param name="gameManager"> �Q�[���}�l�[�W���[�̃N���X </param>
    public void SetTheGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}
