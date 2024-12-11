using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterRotation : MonoBehaviour
    {
        private Vector3 lookDirection;
        private bool returnForward;

        private void FixedUpdate()
        {
            if (GameManager.Instance.startGame)
            {
                lookDirection = new Vector3(JoystickPlayerExample.Instance.variableJoystick.Horizontal, 0, JoystickPlayerExample.Instance.variableJoystick.Vertical);

                if (lookDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(lookDirection);
                }
            }
        }
    }
}