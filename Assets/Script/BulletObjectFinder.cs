using UnityEngine;

/// <summary>
/// ’e‚ÌƒvƒŒƒtƒ@ƒu‚ğ•Ô‚·
/// </summary>
public class BulletObjectFinder : MonoBehaviour
{
    [Header("’e‚ÌPrefabˆê——")]
    [SerializeField] private GameObject normalBullet; // •’Ê‚Ì’e

    /// <summary>
    /// ’e‚Ìí—Ş
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
