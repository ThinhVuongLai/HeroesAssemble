using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class AnimationManager : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (PlayerManager.Instance.isClicking)
            {
                animator.SetBool("Run", true);
                animator.SetBool("Idle", false);
            }
            else
            {
                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
            }
        }
    }
}