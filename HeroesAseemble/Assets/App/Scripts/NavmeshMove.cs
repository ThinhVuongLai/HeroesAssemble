using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HeroesAssemble
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavmeshMove : MonoBehaviour
    {
        private float _moveSmoothing = 0.3f;
        private NavMeshAgent _agent;
        public VariableJoystick variableJoystick;

        [SerializeField] private float joyStickMagnitude;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_agent == null)
            {
                return;
            }

            if (GameManager.Instance.startGame)
            {
                float horizontal = variableJoystick.Direction.x;
                float vertical = variableJoystick.Direction.y;
                joyStickMagnitude = variableJoystick.Direction.magnitude;
                if (variableJoystick.Direction.magnitude >= 0.1f)
                {
                    _agent.updatePosition = true;

                    Vector3 movement = new Vector3(horizontal, 0, vertical);

                    _agent.Move(movement * Time.deltaTime * _agent.speed);
                    this.transform.position = Vector3.Lerp(this.transform.position, _agent.nextPosition, _moveSmoothing);
                }
                else
                {
                    _agent.updatePosition = false;
                }

            }
        }
    }
}