using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    public Vector2 range = new Vector2(4f, 2f);
    Transform mTrans;
    Quaternion mStart;
    Vector2 mRot = Vector2.zero;
    void Start()
    {
        mTrans = transform;
        mStart = mTrans.localRotation;
    }
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        float halfWidth = Screen.width * 0.5f;
        float halfHeight = Screen.height * 0.5f;
        float x = Mathf.Clamp((pos.x - halfWidth) / halfWidth, -0.4f, 0.4f);
        float y = Mathf.Clamp((pos.y - halfHeight) / halfHeight, -0.4f, 0.4f);
        mRot = Vector2.Lerp(mRot, new Vector2(x, y), Time.deltaTime * 2f);
        //旋转方向与鼠标所在方向一致
        mTrans.localRotation = mStart * Quaternion.Euler(-mRot.y * range.y, -mRot.x * range.x, 0f);
    }
}
