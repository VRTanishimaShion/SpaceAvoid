using UnityEngine;
using static Player;

/// <summary>
/// �v���C���[�̊Ǘ�
/// </summary>
public class Player : MonoBehaviour
{
    // �Œ�̐�
    /// <summary>  </summary>

    // �ύX�\�ȕϐ�
    /// <summary> ���� </summary>
    [SerializeField] private float PlayerSpeed = 3f;


    //////////////////////////////////////////////////////////////////////////
    /// <summary>  �v���C���[�̓��͂���������X�N���v�g </summary>
    [SerializeField] private PlayerInputController _playerInputController;

    /// <summary>
    /// ������͈�
    /// </summary>
    public struct MovementRange
    {
        public float up;     // ��
        public float down;   // ��
        public float left;   // ��
        public float right;  // �E
    }
    private MovementRange _movementRange;


    /// <summary> Unity�̋@�\�̏��� </summary>
    public void InitSystem()
    {
        _playerInputController.InitSystem();
    }
    /// <summary> �ϐ��̏������Ȃ� </summary>
    public void Init()
    {
        _movementRange = new MovementRange();
        float width = (transform.localScale.x / 2);
        _movementRange.up       = -width;
        _movementRange.down     = width;
        _movementRange.left     = width;
        _movementRange.right    = -width;

        _playerInputController.Init();
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Movement()
    {
        Vector2 inputVector = _playerInputController.GetInputVector();

        transform.Translate(inputVector * PlayerSpeed * Time.fixedDeltaTime);

        Vector2 position = transform.position;
        position.x = Mathf.Clamp(position.x, _movementRange.left, _movementRange.right);
        position.y = Mathf.Clamp(position.y, _movementRange.down, _movementRange.up);
        transform.position = position;
    }

    /// <summary>
    /// ������͈͂�ݒ肷��
    /// </summary>
    /// <param name="range"> �ǂ̈ʒu </param>
    public void SetTheMovementRange(MovementRange range)
    {
        _movementRange.up += range.up;
        _movementRange.down += range.down;
        _movementRange.left += range.left;
        _movementRange.right += range.right;
    }
    /// <summary>
    /// ������͈͂��擾
    /// </summary>
    /// <returns> ������͈� </returns>
    public MovementRange GetTheMovementRange()
    {
        return _movementRange;
    }
}
