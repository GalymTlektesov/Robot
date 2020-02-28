using UnityEngine;

public class ScalePlus : MonoBehaviour
{
    public MeshRenderer backGround;
    public float plus = 125.0f;
    public float xScale, zScale;

    private void Start()
    {
        backGround = GetComponentInChildren<MeshRenderer>();
    }

    void FixedUpdate()
    {
        if (Time.time < 0.75f)
        {
            xScale = backGround.transform.localScale.x;
            zScale = backGround.transform.localScale.z;
        }
        if (Time.time > 0.75f)
        {
            backGround.transform.localScale = new Vector3(xScale + plus, 1.0f, zScale + plus);
        }
    }
}
