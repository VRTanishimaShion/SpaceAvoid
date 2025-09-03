using UnityEngine;

/// <summary>
/// �v���C���[�̓��͂���������X�N���v�g
/// </summary>
public class PlayerInputController : MonoBehaviour
{
    // �Œ�̐�
    /// <summary> ���삵�Ă���w��ID���Ȃ��ꍇ </summary>
    private const int ResetActiveFingerId = -1;

    // �ύX�\�ȕϐ�
    /// <summary> �v�j�R���̗����ő�l </summary>
    private const float Radius = 100;

    /////////////////////////////////////////////////////
    /// <summary> ����p�p�l�� </summary>
    [SerializeField] private RectTransform controlPanel;
    /// <summary> ���삵�Ă���w��ID </summary>
    private int activeFingerId = -1;
    /// <summary> �G��n�߂��ʒu </summary>
    private Vector2 startPos;

    /// <summary> Unity�̋@�\�̏��� </summary>
    public void InitSystem()
    {

    }
    /// <summary> �ϐ��̏������Ȃ� </summary>
    public void Init()
    {
        
    }
    
    /// <summary>
    /// ���͏����F�^�b�`���삩��Vector2�̓��͕�����Ԃ�
    /// </summary>
    /// <returns> ���͕��� </returns>
    public Vector2 GetInputVector()
    {
        // �V�K�^�b�`�J�n
        if(Input.touchCount > 0)
        {
            // �G��Ă���w�����ꂼ�ꏈ��
            foreach(Touch finger in Input.touches)
            {
                // �G��n�� + ���ɐG��Ă���w���Ȃ�
                if(finger.phase == TouchPhase.Began && activeFingerId == ResetActiveFingerId)
                {
                    // �p�l�����Ȋm�F
                    if(RectTransformUtility.RectangleContainsScreenPoint(controlPanel, finger.position))
                    {
                        // �G�ꂽ�w��Id��ۑ�
                        activeFingerId = finger.fingerId;
                        startPos = finger.position;
                    }
                }

                // �A�N�e�B�u�Ȏw�Ȃ�ǐ�
                if(finger.fingerId == activeFingerId)
                {
                    // �ړ��Œ��܂��͐G�ꂽ�܂ܐÎ~���Ă�����
                    if(finger.phase == TouchPhase.Moved || finger.phase == TouchPhase.Stationary)
                    {
                        Vector2 nowPos = finger.position;
                        return GetInputDirection(nowPos);
                    }
                    // �^�b�`���I��������Ԃ܂��̓L�����Z���������
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

            // �^�b�`���Ȃ��Ƃ��͓��͂Ȃ�
            return Vector2.zero;
    }

    /// <summary>
    /// ���͕������v�Z
    /// </summary>
    /// <param name="currentPos"> ���݂̈ʒu </param>
    /// <returns> ���� </returns>
    private Vector2 GetInputDirection(Vector2 currentPos)
    {
        Vector2 delta = currentPos - startPos;
        delta = Vector2.ClampMagnitude(delta, Radius);
        delta /= Radius;
        return delta;
    }
}
