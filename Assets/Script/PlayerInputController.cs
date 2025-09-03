using UnityEngine;

/// <summary>
/// プレイヤーの入力を処理するスクリプト
/// </summary>
public class PlayerInputController : MonoBehaviour
{
    // 固定の数
    /// <summary> 操作している指のIDがない場合 </summary>
    private const int ResetActiveFingerId = -1;

    // 変更可能な変数
    /// <summary> プニコンの離れる最大値 </summary>
    private const float Radius = 100;

    /////////////////////////////////////////////////////
    /// <summary> 操作用パネル </summary>
    [SerializeField] private RectTransform controlPanel;
    /// <summary> 操作している指のID </summary>
    private int activeFingerId = -1;
    /// <summary> 触れ始めた位置 </summary>
    private Vector2 startPos;

    /// <summary> Unityの機能の処理 </summary>
    public void InitSystem()
    {

    }
    /// <summary> 変数の初期化など </summary>
    public void Init()
    {
        
    }
    
    /// <summary>
    /// 入力処理：タッチ操作からVector2の入力方向を返す
    /// </summary>
    /// <returns> 入力方向 </returns>
    public Vector2 GetInputVector()
    {
        // 新規タッチ開始
        if(Input.touchCount > 0)
        {
            // 触れている指をそれぞれ処理
            foreach(Touch finger in Input.touches)
            {
                // 触れ始め + 他に触れている指がない
                if(finger.phase == TouchPhase.Began && activeFingerId == ResetActiveFingerId)
                {
                    // パネル内科確認
                    if(RectTransformUtility.RectangleContainsScreenPoint(controlPanel, finger.position))
                    {
                        // 触れた指のIdを保存
                        activeFingerId = finger.fingerId;
                        startPos = finger.position;
                    }
                }

                // アクティブな指なら追跡
                if(finger.fingerId == activeFingerId)
                {
                    // 移動最中または触れたまま静止している状態
                    if(finger.phase == TouchPhase.Moved || finger.phase == TouchPhase.Stationary)
                    {
                        Vector2 nowPos = finger.position;
                        return GetInputDirection(nowPos);
                    }
                    // タッチが終了した状態またはキャンセルした状態
                    else if (finger.phase == TouchPhase.Ended || finger.phase == TouchPhase.Canceled)
                    {
                        activeFingerId = ResetActiveFingerId;
                        return Vector2.zero;
                    }
                }
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 nowPos = Input.mousePosition;
            if (RectTransformUtility.RectangleContainsScreenPoint(controlPanel, nowPos))
            {
                activeFingerId = 0;
                return GetInputDirection(nowPos);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            activeFingerId = -1;
        }

            // タッチがないときは入力なし
            return Vector2.zero;
    }

    /// <summary>
    /// 入力方向を計算
    /// </summary>
    /// <param name="currentPos"> 現在の位置 </param>
    /// <returns> 方向 </returns>
    private Vector2 GetInputDirection(Vector2 currentPos)
    {
        Vector2 delta = currentPos - startPos;
        delta = Vector2.ClampMagnitude(delta, Radius);
        delta /= Radius;
        return delta;
    }
}
