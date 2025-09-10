using UnityEngine;

/// <summary>
/// �X�e�[�W�f�[�^
/// </summary>
public class StageData
{
    /// <summary> �ԍ� </summary>
    public int Id { get; private set; }
    /// <summary> ���� </summary>
    public float speed { get; private set; }
    /// <summary> ���� </summary>
    public float spawnTime { get; private set; }
    /// <summary> ���� </summary>
    public Vector2 spawnPosition { get; private set; }
    /// <summary> �e�̊p�x </summary>
    public Vector2 shootAngle { get; private set; }

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    public StageData(string[] values)
    {
        Id              = int.Parse(values[0]);
        speed           = float.Parse(values[1]);
        spawnTime       = float.Parse(values[2]);
        Vector2 vec     = CSVLoader.ParseVector2(values[3]);
        spawnPosition   = vec;
        vec             = CSVLoader.ParseVector2(values[4]);
        shootAngle      = vec;
    }
}
