using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStatus currentPlayerStatus = PlayerStatus.None;

        private List<CharacterController> characterControllers=new List<CharacterController>();

        private List<Transform> enemyTransforms=new List<Transform>();

        private void OnEnable()
        {
            EventController.Instance.ClearCharacterControllers.AddListener(ClearCharacterControllers);
            EventController.Instance.AddCharacterControllerChannel.AddListener(AddCharacterController);
            EventController.Instance.RemoveCharacterControllerChannel.AddListener(RemoveCharacterController);

            EventController.Instance.ClearEnemyTransforms.AddListener(ClearEnemyTransforms);
            EventController.Instance.AddEnemyTransform.AddListener(AddEnemyTransform);
            EventController.Instance.RemoveEnemyTransform.AddListener(RemoveEnemyTransform);

            EventController.Instance.SetEnemyForCharacter.AddListener(SetEnemyForCharacter);

            EventController.Instance.OnPlayerIdle.AddListener(OnPlayerIdle);
            EventController.Instance.OnPlayerMove.AddListener(OnPlayerMove);

            EventController.Instance.GetPlayerStatusChannel.AddListener(GetPlayerStatus);
        }

        private void OnDisable()
        {
            if(EventController.Instance == null)
            {
                return;
            }

            EventController.Instance.ClearCharacterControllers.RemoveListener(ClearCharacterControllers);
            EventController.Instance.AddCharacterControllerChannel.RemoveListener(AddCharacterController);
            EventController.Instance.RemoveCharacterControllerChannel.RemoveListener(RemoveCharacterController);

            EventController.Instance.ClearEnemyTransforms.RemoveListener(ClearEnemyTransforms);
            EventController.Instance.AddEnemyTransform.RemoveListener(AddEnemyTransform);
            EventController.Instance.RemoveEnemyTransform.RemoveListener(RemoveEnemyTransform);

            EventController.Instance.SetEnemyForCharacter.RemoveListener(SetEnemyForCharacter);

            EventController.Instance.OnPlayerIdle.RemoveListener(OnPlayerIdle);
            EventController.Instance.OnPlayerMove.RemoveListener(OnPlayerMove);

            EventController.Instance.GetPlayerStatusChannel.RemoveListener(GetPlayerStatus);
        }

        private void AddCharacterController(CharacterController characterController)
        {
            characterControllers.Add(characterController);
        }

        private void RemoveCharacterController(CharacterController characterController)
        {
            if(characterControllers.Contains(characterController))
            {
                characterControllers.Remove(characterController);
            }
        }

        private void ClearCharacterControllers()
        {
            characterControllers.Clear();
        }

        private void AddEnemyTransform(Transform enemyTransform)
        {
            if(!enemyTransforms.Contains(enemyTransform))
            {
                enemyTransforms.Add(enemyTransform);
            }
        }

        private void RemoveEnemyTransform(Transform enemyTransform)
        {
            if(enemyTransforms.Contains(enemyTransform))
            {
                enemyTransforms.Remove(enemyTransform);
            }
        }

        private void ClearEnemyTransforms()
        {
            enemyTransforms.Clear();
        }

        private void SetEnemyForCharacter()
        {
            Transform enemyTransform = null;
            CharacterController currentCharacterController = null;
            for(int i = 0, length = characterControllers.Count; i<length; i++)
            {
                currentCharacterController = characterControllers[i];
                if(!currentCharacterController.HasTargetEnemy)
                {
                    enemyTransform = GetEnemyTransformsInList(currentCharacterController.transform);

                    if(enemyTransform != null)
                    {
                        currentCharacterController.SetEnemy(enemyTransform.gameObject);
                    }
                }
            }
        }

        private Transform GetEnemyTransformsInList(Transform characterTransform)
        {
            Transform returnTransform = null;

            float distance = 0;
            float minDistance = -1;
            for(int i = 0, length = enemyTransforms.Count; i<length; i++)
            {
                distance = Vector3.Distance(characterTransform.position,enemyTransforms[i].position);

                if(minDistance < distance)
                {
                    minDistance = distance;
                    returnTransform = enemyTransforms[i];
                }
            }

            return returnTransform;
        }

        private void OnPlayerIdle()
        {
            if(currentPlayerStatus == PlayerStatus.Idle)
            {
                return;
            }

            currentPlayerStatus = PlayerStatus.Idle;

            SetEnemyForCharacter();
        }

        private void OnPlayerMove()
        {
            if(currentPlayerStatus == PlayerStatus.Move)
            {
                return;
            }

            currentPlayerStatus = PlayerStatus.Move;
            SetAllCharacterToMove();
        }

        private PlayerStatus GetPlayerStatus()
        {
            return currentPlayerStatus;
        }

        private void SetAllCharacterToMove()
        {
            for(int i = 0, length = characterControllers.Count; i<length; i++)
            {
                characterControllers[i].SetEnemy(null);
            }
        }
    }

    public enum PlayerStatus
    {
        None = -1,
        Idle,
        Move,
    }
}

