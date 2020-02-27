using UnityEngine;

public class ScalePlus : MonoBehaviour
{
    public GameObject backGround;
    public float plus = 125.0f;
    public float xScale, zScale;
    public bool isPlus = false;

    void Update()
    {       
        backGround = GameObject.Find("BackgroundPlane");
        backGround.transform.localScale = new Vector3(1123.644f, backGround.transform.localScale.y, 873.9829f);
        //if (backGround != null)
        //{
        //    xScale = backGround.transform.localScale.x;
        //    zScale = backGround.transform.localScale.z;
        //}
    }

    //private void FixedUpdate()
    //{
    //    if (isPlus == false && xScale == backGround.transform.localScale.x)
    //    {
    //        backGround.transform.localScale = new Vector3(xScale + plus, backGround.transform.localScale.y, zScale + plus);
    //        isPlus = true;
    //    }
    //}
}
