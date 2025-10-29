using JetBrains.Annotations;
using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 弾の標準型
/// </summary>
public class Bullet_Base : MonoBehaviour
{
    /// <summary> スプライトレンダラー </summary>
    [SerializeField] private SpriteRenderer spriteRenderer;

    /// <summary> 枠の位置 </summary>
    private FramePosition framePos = new FramePosition();

    /// <summary> 非アクティブ化するためのコールバック </summary>
    private Action _onDisable;

    /// <summary> 当たっているか </summary>
    public bool isCollider { get; private set; }
    /// <summary> 壊れている </summary>
    public bool isBreak{ get; private set; }
    
    /// <summary> 出現オフセット値 </summary>
    private float spawnTimeOffset;
    /// <summary> 予兆データ </summary>
    private List<float> preEffect;
    /// <summary> 速さ </summary>
    private float speed;
    /// <summary> 角度 </summary>
    private Vector2 angle;
    /// <summary> 枠 </summary>
    private Player.MovementRange range;
    /// <summary> 現在の時間 </summary>
    private float nowTime = 0;

    private const float FrameOrSecond = 1.0f;

    /// <summary>
    /// 弾の状態
    /// </summary>
    public enum BulletState
    {
        Offset,
        PreEffect,
        Moving,
    }
    private BulletState bulletState;
    public void SetTheBulletState(BulletState state) {  bulletState = state; }
    
    /// <summary>
    /// 弾の色の状態
    /// </summary>
    public enum BulletColorState
    {
        Red,
        White,
    }
    private BulletColorState bulletColorState;
    public void SetTheBulletColorState(BulletColorState state) { bulletColorState = state; }
    private int changeColorNumber;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="bulletStatus"> 弾のステータス </param>
    public void Initialize(Bullet_BaseStatus bulletStatus, Action onDisable)
    {
        spriteRenderer.sprite = bulletStatus.sprite;
        spriteRenderer.color = Color.white;
        transform.localScale = bulletStatus.scale;
        spawnTimeOffset = bulletStatus.spawnTimeOffset;
        preEffect = bulletStatus.preEffect;
        speed = bulletStatus.speed;
        transform.position = bulletStatus.worldPosition;
        angle = bulletStatus.angle;
        range = bulletStatus.range;


        Vector3 angleObj = transform.eulerAngles;
        angleObj.z = (Mathf.Atan2(bulletStatus.angle.y, bulletStatus.angle.x) * Mathf.Rad2Deg) + 270;
        transform.eulerAngles = angleObj;

        _onDisable = onDisable;
        isBreak = false;
        isCollider = false;

        changeColorNumber = 0;
        nowTime = 0;
        bulletState = BulletState.Offset;
        bulletColorState = BulletColorState.Red;

        // テスト用
        framePos.up = 5;
        framePos.down = -5;
        framePos.left = -10;
        framePos.right = 10;

        // 撃つ場所に移動する
        while(true)
        {
            Vector2 pos = transform.position;

            if (pos.y >= framePos.up || pos.y <= framePos.down ||
                    pos.x >= framePos.right || pos.x <= framePos.left)
            {
                Break();
                break;
            }

            pos = pos + (GetMoveDirection() * speed * Time.deltaTime);

            if(pos.y <= range.up && pos.y >= range.down &&
               pos.x >= range.left && pos.x <= range.right)
            {
                transform.position = pos;
                break;
            }

            transform.position = pos;
        }
    }

    /// <summary>
    /// 動きに関するもの
    /// </summary>
    public void Movement()
    {
        nowTime++;

        switch(bulletState)
        {
            case BulletState.Offset:
                if(nowTime >= (spawnTimeOffset) * FrameOrSecond)
                {
                    Debug.Log("打てます");
                    SetTheBulletState(BulletState.PreEffect);
                    nowTime = 0;
                }
                break;
            case BulletState.PreEffect:
                if(preEffect.Count <= changeColorNumber)
                {
                    SetTheBulletState(BulletState.Moving);
                    break;
                }

                if(nowTime >= (preEffect[changeColorNumber] * FrameOrSecond))
                {
                    switch(bulletColorState)
                    {
                        case BulletColorState.Red:
                            Debug.Log("赤に変わる");
                            spriteRenderer.color = Color.red;
                            SetTheBulletColorState(BulletColorState.White);
                            break;
                        case BulletColorState.White:
                            Debug.Log("白に変わる");
                            spriteRenderer.color = Color.white;
                            SetTheBulletColorState(BulletColorState.Red);
                            break;
                    }
                    changeColorNumber++;
                    nowTime = 0;
                }

                break;
            case BulletState.Moving:
                Vector2 pos = transform.position;
                pos = pos + (GetMoveDirection() * speed * Time.deltaTime);
                transform.position = pos;

                if (pos.y >= framePos.up || pos.y <= framePos.down ||
                    pos.x >= framePos.right || pos.x <= framePos.left)
                {
                    Break();
                }
                break;
        }

        // ここに予兆データとオフセット値の機能を追加する

        
    }

    /// <summary>
    /// 動きの向きに関する制御
    /// </summary>
    /// <returns> 向き </returns>
    public Vector2 GetMoveDirection()
    {
        Vector2 direction = angle;

        return direction;
    }

    /// <summary>
    /// 壊れる
    /// </summary>
    public void Break()
    {
        if (_onDisable == null) return;

        _onDisable?.Invoke();
        isBreak = true;

        _onDisable = null;
    }

    /// <summary>
    /// 見た目を消すための処理
    /// </summary>
    public void CallBreak()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 当たったら
    /// </summary>
    public void OnHit()
    {
        spriteRenderer.sprite = null;
        isCollider = true;
    }
}
