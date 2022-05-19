using UnityEngine;

public class HealEffect : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("Destroy", 0.7f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
