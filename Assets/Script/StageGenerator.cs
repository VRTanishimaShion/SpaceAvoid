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
    [SerializeField] GameObject[] wallGenerator;

    /// <summary> Unity�̋@�\�̏��� </summary>
    public void InitSystem()
    {

    }
    /// <summary> �ϐ��̏������Ȃ� </summary>
    public void Init()
    {

    }

    /// <summary>
    /// ������͈͂�}������
    /// </summary>
    /// <param name="range"> �͈� </param>
    public void SetTheMovementRange(ref Player.MovementRange range)
    {
        float width = (wallGenerator[0].transform.localScale.y / 2);

        range.up    = (wallGenerator[0].transform.position.y - width);
        range.down  = (wallGenerator[1].transform.position.y + width);
        range.left  = (wallGenerator[2].transform.position.x + width);
        range.right = (wallGenerator[3].transform.position.x - width);
    }
}
