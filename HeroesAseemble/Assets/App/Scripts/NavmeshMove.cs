using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HeroesAssemble
{
    public class NavmeshMove : MonoBehaviour
    {
        private float _moveSmoothing = 0.3f;
        private NavMeshAgent _agent;
        public VariableJoystick variableJoystick;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();

            _agent.updatePosition = false;
        }

        private void Update()
        {
            if (GameManager.Instance.startGame)
            {
                float horizontal = variableJoystick.Direction.x;
                float vertical = variableJoystick.Direction.y;
                if (variableJoystick.Direction.magnitude >= 0.45)
                {
                    Vector3 movement = new Vector3(horizontal, 0, vertical);

                    _agent.Move(movement * Time.deltaTime * _agent.speed);
                }

                this.transform.position = Vector3.Lerp(this.transform.position, _agent.nextPosition, _moveSmoothing);
            }
        }
    }
}