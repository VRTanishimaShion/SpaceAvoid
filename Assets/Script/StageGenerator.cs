using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �X�e�[�W�̊Ǘ�
/// </summary>
public class StageGenerator : MonoBehaviour
{
    // �Œ�̐�
    /// <summary>  </summary>

    // �ύX�\�ȕϐ�
    /// <summary>  </summary>


    //////////////////////////////////////////////////////////////////////////
    /// <summary> �ǁi�I�u�W�F�N�g�j�̊Ǘ� </summary>
    [SerializeField] private GameObject[] wallGenerator;
    /// <summary> �X�e�[�W�͈̔� </summary>
    private Player.MovementRange stageRange;

    /// <summary> Unity�̋@�\�̏��� </summary>
    public void InitSystem()
    {
        
    }
    /// <summary> �ϐ��̏������Ȃ� </summary>
    public void Init()
    {
        stageRange = new Player.MovementRange();

        float width = (wallGenerator[0].transform.localScale.y / 2);
        stageRange.up = (wallGenerator[0].transform.position.y - width);
        stageRange.down = (wallGenerator[1].transform.position.y + width);

        width = (wallGenerator[2].transform.localScale.x / 2);
        stageRange.left = (wallGenerator[2].transform.position.x + width);
        stageRange.right = (wallGenerator[3].transform.position.x - width);
    }

    /// <summary>
    /// ������͈͂�}������
    /// </summary>
    /// <param name="range"> �͈� </param>
    public void SetTheMovementRange(ref Player.MovementRange range)
    {
        range = stageRange;
    }
    /// <summary>
    /// �X�e�[�W�͈̔͂�Ԃ�
    /// </summary>
    /// <returns> �X�e�[�W�͈̔� </returns>
    public Player.MovementRange GetTheMovementRange()
    {
        return stageRange;
    }
}
