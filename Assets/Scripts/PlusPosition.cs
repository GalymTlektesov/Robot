using UnityEngine;

public class PlusPosition : MonoBehaviour
{
    public MeshRenderer backGround;
    public float plus = 125.0f;
    public float zPos;

    void FixedUpdate()
    {
        if (Time.time < 0.75f)
        {
            backGround = GetComponentInChildren<MeshRenderer>();
            zPos = backGround.transform.position.z;
        }
        if (Time.time > 0.75f)
        {
            backGround.transform.position = new Vector3(backGround.transform.position.x,
                backGround.transform.position.y, zPos - plus);
        }
    }
}
