using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ���̊�{�ƂȂ�݌v(��)
/// </summary>
public class BulletBase : MonoBehaviour
{
    // �e�̑��x
    public float speed;
    // �p�x
    public Vector2 flightAngle;

    /// <summary> Unity�̋@�\�̏��� </summary>
    public void InitSystem()
    {

    }
    /// <summary> �ϐ��̏������Ȃ� </summary>
    public void Init(float speed, Vector2 launchAngle)
    {
        this.speed = speed;
        flightAngle = launchAngle;
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Movement()
    {
        transform.Translate(flightAngle * speed * Time.fixedDeltaTime);
    }
}
