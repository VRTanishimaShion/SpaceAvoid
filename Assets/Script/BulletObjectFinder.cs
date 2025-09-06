using UnityEngine;

/// <summary>
/// �e�̃v���t�@�u��Ԃ�
/// </summary>
public class BulletObjectFinder : MonoBehaviour
{
    [Header("�e��Prefab�ꗗ")]
    [SerializeField] private GameObject normalBullet; // ���ʂ̒e

    /// <summary>
    /// �e�̎��
    /// </summary>
    public enum BulletType
    {
        Normal,
    }

    public GameObject GetBullet(BulletType type)
    {
        switch(type)
        {
            case BulletType.Normal:
                return normalBullet;
        }

        return null;
    }
}
