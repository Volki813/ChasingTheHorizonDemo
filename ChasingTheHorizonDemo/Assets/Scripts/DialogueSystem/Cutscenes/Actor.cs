using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    public Image portrait;
    public Animator animator;

    public void CreateActor(string name, Image portrait, Animator animator, RuntimeAnimatorController animatorController)
    {
        this.name = name;
        this.portrait = portrait;
        this.animator = animator;
        animator.runtimeAnimatorController = animatorController;
    }
}
