using UnityEngine;

public class ScreenDim : MonoBehaviour
{
    public Animator animator = null;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
