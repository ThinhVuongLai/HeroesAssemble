using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class EventController : SceneDependentSingleton<EventController>
    {
        [Header("Void Channel")]
        [SerializeField] private VoidChannel updateAssemplyChannel;
        [SerializeField] private VoidChannel clearCharacterControllers;
        [SerializeField] private VoidChannel clearEnemyTransforms;
        [SerializeField] private VoidChannel setEnemyForCharacter;
        [SerializeField] private VoidChannel onPlayerIdle;
        [SerializeField] private VoidChannel onPlayerMove;
        [SerializeField] private VoidChannel pauseGameChannel;

        [Header("GetCharacterInforChannel")]
        [SerializeField] private GetCharacterInforChannel getCharacterInforChannel;

        [Header("IntegerChannel")]
        [SerializeField] private IntegerChannel addCharacterChannel;
        [SerializeField] private IntegerChannel removeCharacterChannel;

        [Header("CharacterControllerChannel")]
        [SerializeField] private CharacterControllerChannel addCharacterControllerChannel;
        [SerializeField] private CharacterControllerChannel removeCharacterControllerChannel;

        [Header("TransformChannel")]
        [SerializeField] private TransformChannel addEnemyTransform;
        [SerializeField] private TransformChannel removeEnemyTransform;

        [Header("GetPlayerStatusChannel")]
        [SerializeField] private GetPlayerStatusChannel getPlayerStatusChannel;

        // Void Channel
        public VoidChannel UpdateAssemplyChannel => updateAssemplyChannel;
        public VoidChannel ClearCharacterControllers => clearCharacterControllers;
        public VoidChannel ClearEnemyTransforms => clearEnemyTransforms;
        public VoidChannel SetEnemyForCharacter => setEnemyForCharacter;
        public VoidChannel OnPlayerIdle => onPlayerIdle;
        public VoidChannel OnPlayerMove => onPlayerMove;
        public VoidChannel PauseGameChannel => pauseGameChannel;

        // GetCharacterInforChannel
        public GetCharacterInforChannel GetCharacterInforChannel => getCharacterInforChannel;

        // IntegerChannel
        public IntegerChannel AddCharacterChannel => addCharacterChannel;
        public IntegerChannel RemoveCharacterChannel => removeCharacterChannel;

        // CharacterControllerChannel
        public CharacterControllerChannel AddCharacterControllerChannel => addCharacterControllerChannel;
        public CharacterControllerChannel RemoveCharacterControllerChannel => removeCharacterControllerChannel;

        // TransformChannel
        public TransformChannel AddEnemyTransform => addEnemyTransform;
        public TransformChannel RemoveEnemyTransform => removeEnemyTransform;

        // GetPlayerStatusChannel
        public GetPlayerStatusChannel GetPlayerStatusChannel => getPlayerStatusChannel;
    }
}

