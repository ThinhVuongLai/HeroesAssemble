using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeroesAssemble
{
    [CreateAssetMenu(fileName ="TransformChannel",menuName ="ScriptableObject/Channel/TransformChannel")]
    public class TransformChannel : ScriptableObject
    {
        [SerializeField] private UnityEvent<Transform> _channel;

        public void AddListener(UnityAction<Transform> action)
        {
            _channel.AddListener(action);
        }

        public void RemoveListener(UnityAction<Transform> action)
        {
            _channel.RemoveListener(action);
        }

        public void RunTransformChannel(Transform inputValue)
        {
            _channel?.Invoke(inputValue);
        }
    }
}

