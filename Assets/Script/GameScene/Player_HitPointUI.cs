using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーの体力のUI
/// </summary>
public class Player_HitPointUI : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの体力UIのオブジェクト
    /// </summary>
    [SerializeField] private GameObject[] hitPointImage;

    /// <summary>
    /// リセット、アクティブ状態に戻す
    /// </summary>
    public void ResetActive()
    {
        foreach(GameObject obj in hitPointImage)
        {
            if(!obj.activeSelf)
            {
                obj.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 体力を減らす
    /// </summary>
    public void TakeDamage(int currentHitPoint)
    {
        if(currentHitPoint < 0) { return; }

        if (!hitPointImage[currentHitPoint].activeSelf) { return; }

        hitPointImage[currentHitPoint].SetActive(false);
    }
}
