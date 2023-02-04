// ScratchUI.cs

using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 刮刮乐UI
/// </summary>
public class ScratchUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    /// <summary>
    /// 绘制的目标图片
    /// </summary>
    public RenderTexture renderTexture;
    /// <summary>
    /// 笔刷
    /// </summary>
    public Texture brushTexture;

    /// <summary>
    /// 空白图
    /// </summary>
    public Texture blankTexture;

    /// <summary>
    /// mask的RectTransform
    /// </summary>
    public RectTransform rectTransform;
    /// <summary>
    /// 画布
    /// </summary>
    public Canvas canvas;

    private bool m_isMove = false;

    private void Start()
    {

        DrawBlank();
    }

    /// <summary>
    /// 初始化RenderTexture
    /// </summary>
    private void DrawBlank()
    {

        // 激活rt
        RenderTexture.active = renderTexture;
        // 保存当前状态
        GL.PushMatrix();
        // 设置矩阵
        GL.LoadPixelMatrix(0, renderTexture.width, renderTexture.height, 0);

        // 绘制贴图
        Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
        Graphics.DrawTexture(rect, blankTexture);

        // 弹出改变
        GL.PopMatrix();

        RenderTexture.active = null;
    }

    /// <summary>
    /// 在RenderTexture的(x,y)坐标处画笔刷图案
    /// </summary>
    /// <param name="x">Graphics坐标系下的x</param>
    /// <param name="y">Graphics坐标系下的y</param>
    private void Draw(int x, int y)
    {

        // 激活rt
        RenderTexture.active = renderTexture;
        // 保存当前状态
        GL.PushMatrix();
        // 设置矩阵
        GL.LoadPixelMatrix(0, renderTexture.width, renderTexture.height, 0);


        // 绘制笔刷图案
        x -= (int)(brushTexture.width * 0.5f);
        y -= (int)(brushTexture.height * 0.5f);
        Rect rect = new Rect(x, y, brushTexture.width, brushTexture.height);
        Graphics.DrawTexture(rect, brushTexture);

        // 弹出改变
        GL.PopMatrix();

        RenderTexture.active = null;
    }

    /// <summary>
    /// 按下
    /// </summary>
    public void OnPointerDown(PointerEventData data)
    {

        m_isMove = true;
    }

    /// <summary>
    /// 抬起
    /// </summary>
    public void OnPointerUp(PointerEventData data)
    {

        m_isMove = false;
    }

    private void Update()
    {

        if (m_isMove)
        {

            OnMouseMove(Input.mousePosition);
        }
    }

    /// <summary>
    /// 刮卡
    /// </summary>
    /// <param name="position">刮卡的屏幕坐标</param>
    private void OnMouseMove(Vector2 position)
    {

        // 获取刮的位置的ui局部坐标
        var uiLocalPos = ScreenPosToUiLocalPos(position, rectTransform, canvas.worldCamera);
        // 将局部坐标转化为uv坐标
        var uvX = (rectTransform.sizeDelta.x / 2f + uiLocalPos.x) / rectTransform.sizeDelta.x;
        var uvY = (rectTransform.sizeDelta.y / 2f + uiLocalPos.y) / rectTransform.sizeDelta.y;
        // 将uv坐标转化为Graphics坐标
        var x = (int)(uvX * renderTexture.width);
        // 注意，uv坐标系和Graphics坐标系的y轴方向相反
        var y = (int)(renderTexture.height - uvY * renderTexture.height);

        Draw(x, y);
    }

    /// <summary>
    /// 将屏幕坐标抓话为目标RectTransform的局部坐标
    /// </summary>
    /// <param name="screenPos">屏幕坐标</param>
    /// <param name="transform">目标RectTransform</param>
    /// <param name="cam">摄像机</param>
    /// <returns>ui局部坐标</returns>
    private Vector2 ScreenPosToUiLocalPos(Vector3 screenPos, RectTransform transform, Camera cam)
    {

        Vector2 uiLocalPos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(transform, screenPos, cam, out uiLocalPos))
        {

            return uiLocalPos;
        }
        return Vector2.zero;
    }
}