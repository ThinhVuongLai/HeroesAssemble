using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeroesAssemble
{
    [CreateAssetMenu(fileName ="IntegerChannel",menuName ="ScriptableObject/Channel/IntegerChannel")]
    public class IntegerChannel : ScriptableObject
    {
        private UnityEvent<int> _channel;

        public void AddListener(UnityAction<int> action)
        {
            _channel.AddListener(action);
        }

        public void RemoveListener(UnityAction<int> action)
        {
            _channel.RemoveListener(action);
        }

        public void RunIntegerChannel(int inputValue)
        {
            _channel?.Invoke(inputValue);
        }
    }
}