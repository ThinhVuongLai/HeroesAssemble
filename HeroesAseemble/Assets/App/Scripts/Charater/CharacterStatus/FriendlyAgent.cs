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
        private CharacterBase characterBase;
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
            characterBase = GetComponent<CharacterBase>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        /*private void OnEnable()
        {
            if (navMeshAgent)
            {
                navMeshAgent.enabled = true;
            }
        }*/

        private void OnDisable()
        {
            if (navMeshAgent)
            {
                StopNavMeshAgent();
                //navMeshAgent.enabled = false;
            }
        }

        public void SetTargetToMove(GameObject targetObject)
        {
            targetToMove=targetObject;
        }

        public bool IsEnoughDistanceToTarget(float offsetDistance = 0f)
        {
            if(target == null)
            {
                return true;
            }

            float distance = GetDistanceToTarget();

            return distance <= navMeshAgent.stoppingDistance + offsetDistance;
        }

        public bool IsEnoughDistanceToAttack()
        {
            float distanceToTarget = GetDistanceToTarget();

            return distanceToTarget <= characterBase.CurrentCharacterInfor.beginAttackDistance;
        }

        public bool IsTooNearTarget()
        {
            float distanceToTarget = GetDistanceToTarget();

            return distanceToTarget < characterBase.CurrentCharacterInfor.beginAttackDistance;
        }

        public void MoveWhenTooNearTarget()
        {
            Vector3 characterPosition = transform.position;
            Vector3 targetPosition = target.transform.position;

            Vector3 direction = GetDirection(characterPosition, targetPosition);
            
            Vector3 targetMovePosition=target.transform.position + (direction*(characterBase.CurrentCharacterInfor.beginAttackDistance - 0.1f));

            navMeshAgent.SetDestination(targetMovePosition);
        }

        public Vector3 GetDirection(Vector3 from, Vector3 to)
        {
            return (to - from).normalized;
        }

        public float GetDistanceToTarget()
        {
            if (target)
            {
                //return Vector3.Distance(transform.position, target.transform.position);
                return Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.transform.position.x, target.transform.position.z));

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