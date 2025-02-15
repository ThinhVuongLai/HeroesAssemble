using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroesAssemble;

public class JoystickPlayerExample : SceneDependentSingleton<JoystickPlayerExample>
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    private void Update()
    {
        
    }

    public void FixedUpdate()
    {
        if (PlayerManager.Instance.isClicking)
        {
            if (variableJoystick.Direction.magnitude >= 0.45)
            {

                rb.velocity = new Vector3(variableJoystick.Direction.x * speed, rb.velocity.y, variableJoystick.Direction.y * speed);
                GetComponent<Animator>().SetBool("Run", true);
                GetComponent<Animator>().SetBool("Idle", false);
            }
            

        }
        else
        {
            rb.velocity = Vector3.zero;

            GetComponent<Animator>().SetBool("Run", false);
            GetComponent<Animator>().SetBool("Idle", true);

        }
    }
}