using UnityEngine;

/// <summary>
/// ’e‚Ìî•ñ‚O‚P
/// </summary>
public class BulletInfo01Data
{
    /// <summary> ’e‚Ìí—Ş </summary>
    public BulletType _bulletType { get; private set; }
    /// <summary> ‘å‚«‚³ </summary>
    public Vector2 scale { get; private set; }

    /// <summary>
    /// ƒRƒ“ƒXƒgƒ‰ƒNƒ^
    /// </summary>
    public BulletInfo01Data(string[] values)
    {
        _bulletType = BulletType.Cube;
        Vector2 vec = CSVLoader.ParseVector2(values[1]);
        scale       = vec;
    }
}
