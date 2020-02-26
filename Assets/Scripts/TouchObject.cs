using UnityEngine;
public class TouchObject : MonoBehaviour
{
    [Range(0, 8)]
    public int takeNumber = 0;
    internal static int numberAnim = 0;
    private Animator animator;
    private MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        animator = GetComponentInParent<Animator>();
    }

    public void OnMouseDown()
    {
        numberAnim = takeNumber;
        animator.SetInteger("Take", numberAnim);
    }
}