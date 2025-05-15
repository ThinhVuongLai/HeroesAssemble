using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class PlayerCharacterRotation : MonoBehaviour
    {
        [SerializeField] private VariableJoystick variableJoystick;

        private Vector3 lookDirection;
        private bool returnForward;

        private void FixedUpdate()
        {
            if (variableJoystick == null)
            {
                return;
            }

            if (GameManager.Instance.startGame)
            {
                lookDirection = new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical);

                if (lookDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(lookDirection);
                }
            }
        }
    }
}