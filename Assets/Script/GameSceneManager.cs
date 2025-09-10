using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���V�[���̊Ǘ�
/// </summary>
public class GameSceneManager : MonoBehaviour
{
    /// <summary>
    /// �e�̊Ǘ�
    /// </summary>
    private List<BulletBase> bulletGenerator = new List<BulletBase>();

    /// <summary> �X�e�[�W���Ǘ�����N���X </summary>
    [SerializeField] private StageGenerator _stageGenerator;
    /// <summary> �e�̃I�u�W�F�N�g��T���N���X </summary>
    [SerializeField] private BulletObjectFinder _bulletObjectFinder;
    /// <summary> �Q�[���S�̂̏�ԊǗ� </summary>
    private GameManager _gameManager;

    /// <summary>
    /// �e�̏��
    /// </summary>
    public struct bulletInfo
    {
        public int time;
        public Vector2 spawnPosition;
        public Vector2 launchAngle;
    }
   
    /// <summary> Unity�̋@�\�̏��� </summary>
    public void InitSystem()
    {
        _stageGenerator.InitSystem();
    }
    /// <summary> �ϐ��̏������Ȃ� </summary>
    public void Init()
    {
        _stageGenerator.Init();
    }

    /// <summary>
    /// �Q�[���V�[���̎�����
    /// </summary>
    public void GameSceneDPS(int tm)
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            SpawnBullet(BulletObjectFinder.BulletType.Normal, new Vector2(-1, -1), 3f, new Vector2(0, 1));
        }

        foreach (BulletBase bullet in bulletGenerator)
        {
            bullet.Movement();
        }

        // �t�������邱�Ƃō폜�ɂ��Ή�
        //for(int bulletNumber = bulletGenerator.Count - 1; bulletNumber >= 0; bulletNumber--)
        //{
        //    BulletBase bullet = bulletGenerator[bulletNumber];
        //    bullet.Movement();

            
        //}
    }

    /// <summary>
    /// �e�𐶂ݏo��
    /// </summary>
    public void SpawnBullet(BulletObjectFinder.BulletType bulletType,Vector2 spawnPosition, float speed, Vector2 launchAngle)
    {
        GameObject instance = Instantiate(_bulletObjectFinder.GetBullet(bulletType),spawnPosition,Quaternion.identity);
        BulletBase bullet = instance.GetComponent<BulletBase>();
        bullet.SetTheInit(speed, launchAngle);
        bulletGenerator.Add(bullet);
    }

    /// <summary>
    /// ������͈͂�}������
    /// </summary>
    /// <param name="range"> �͈� </param>
    public void SetTheMovementRange(ref Player.MovementRange range)
    {
        _stageGenerator.SetTheMovementRange(ref range);
    }

    /// <summary> �Q�[���S�̂̏�ԊǗ���}�� </summary>
    public void SetTheGameManager(GameManager gameManager) { _gameManager = gameManager;}
}
