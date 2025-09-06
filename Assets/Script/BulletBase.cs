using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ���̊�{�ƂȂ�݌v(��)
/// </summary>
public class BulletBase : MonoBehaviour
{
    // �e�̑��x
    private float speed;
    // �p�x
    private Vector2 flightAngle;

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="speed"> ���� </param>
    /// <param name="launchAngle"> ���ˊp�x </param>
    public BulletBase(float speed, Vector2 launchAngle)
    {
        this.speed = speed;
        flightAngle = launchAngle;
    }

    /// <summary> Unity�̋@�\�̏��� </summary>
    public void InitSystem()
    {

    }
    /// <summary> �ϐ��̏������Ȃ� </summary>
    public void Init()
    {
        
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Movement()
    {
        transform.Translate(flightAngle * speed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// �I�u�W�F�N�g���폜
    /// </summary>
    public void DeleteObject()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="speed"> ���� </param>
    /// <param name="launchAngle"> ���ˊp�x </param>
    public void SetTheInit(float speed, Vector2 launchAngle)
    {
        this.speed = speed;
        flightAngle = launchAngle;
    }
}
