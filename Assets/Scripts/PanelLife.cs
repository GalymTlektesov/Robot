using UnityEngine;

public class PanelLife : MonoBehaviour
{
    public float timeLife = 0.75f;

    void FixedUpdate()
    {
        if(Time.time > timeLife)
        {
            gameObject.SetActive(false);
        }
    }
}
