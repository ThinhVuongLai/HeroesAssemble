using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace HeroesAssemble
{
    public class FriendlyAgent : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        private CharacterController characterController;
        private NavMeshAgent navMeshAgent;
        private bool hasTargetEnemy;
        private GameObject targetToMove;

        public GameObject Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void SetTargetToMove(GameObject targetObject)
        {
            targetToMove=targetObject;
        }

        public bool IsEnoughDisTanceToTarget()
        {
            if(target==null)
            {
                return false;
            }

            float distance = GetDistanceToTarget();

            return distance <= navMeshAgent.stoppingDistance + 0.5f;
        }

        public bool IsEnoughDistanceToAttack()
        {
            float distanceToTarget = GetDistanceToTarget();

            return distanceToTarget <= characterController.CurrentCharacterInfor.beginAttackDistance;
        }

        public bool IsTooNearTarget()
        {
            float distanceToTarget = GetDistanceToTarget();

            return distanceToTarget < characterController.CurrentCharacterInfor.beginAttackDistance;
        }

        public void MoveWhenTooNearTarget()
        {
            Vector3 characterPosition = transform.position;
            Vector3 targetPosition = target.transform.position;

            Vector3 direction = GetDirection(characterPosition, targetPosition);
            
            Vector3 targetMovePosition=target.transform.position + (direction*(characterController.CurrentCharacterInfor.beginAttackDistance - 0.1f));

            navMeshAgent.SetDestination(targetMovePosition);
        }

        public Vector3 GetDirection(Vector3 from, Vector3 to)
        {
            return (to - from).normalized;
        }

        public float GetDistanceToTarget()
        {
            if(target)
            {
                return Vector3.Distance(transform.position, target.transform.position);
            }
            else
            {
                return int.MaxValue;
            }
        }

        public bool HasTarget()
        {
            return target != null;
        }

        public void RunToTarget()
        {
            if(target)
            {
                navMeshAgent.SetDestination(target.transform.position);
            }   
        }

        public void StopNavMeshAgent()
        {
            navMeshAgent.isStopped = true;
        }

        public void ContinueNavMeshAgent()
        {
            navMeshAgent.isStopped = false;
        }

        public void SetTargetToTargetMove()
        {
            target = targetToMove;
        }

        public void LookAtTarget()
        {
            if(target == null)
            {
                return;
            }

        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navMeshAgent.angularSpeed);
        }
    }
}