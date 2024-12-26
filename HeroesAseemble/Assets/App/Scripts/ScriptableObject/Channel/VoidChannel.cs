using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeroesAssemble
{
    [CreateAssetMenu(fileName ="VoidChannel",menuName ="ScriptableObject/Channel/VoidChannel")]
    public class VoidChannel : ScriptableObject
    {
        private UnityEvent _channel;

        public void AddListener(UnityAction action)
        {
            _channel.AddListener(action);
        }

        public void RemoveListener(UnityAction action)
        {
            _channel.RemoveListener(action);
        }

        public void RunVoidChannel()
        {
            _channel?.Invoke();
        }
    }
}