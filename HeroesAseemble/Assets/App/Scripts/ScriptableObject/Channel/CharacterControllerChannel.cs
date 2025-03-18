using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeroesAssemble
{
    [CreateAssetMenu(fileName ="CharacterControllerChannel",menuName ="ScriptableObject/Channel/CharacterControllerChannel")]
    public class CharacterControllerChannel : ScriptableObject
    {
        private UnityEvent<CharacterController> _channel;

        public void AddListener(UnityAction<CharacterController> action)
        {
            _channel.AddListener(action);
        }

        public void RemoveListener(UnityAction<CharacterController> action)
        {
            _channel.RemoveListener(action);
        }

        public void RunCharacterControllerChannel(CharacterController inputValue)
        {
            _channel?.Invoke(inputValue);
        }
    }
}

