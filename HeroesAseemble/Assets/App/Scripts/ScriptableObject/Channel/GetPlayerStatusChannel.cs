using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    [CreateAssetMenu(fileName = "GetPlayerStatusChannel", menuName = "ScriptableObject/Channel/GetPlayerStatusChannel")]
    public class GetPlayerStatusChannel : ScriptableObject
    {
        private Func<PlayerStatus> _func;

        public void AddListener(Func<PlayerStatus> func)
        {
            _func += func;
        }

        public void RemoveListener(Func<PlayerStatus> func)
        {
            _func -= func;
        }

        public PlayerStatus RunGetPlayerStatusChannel()
        {
            PlayerStatus returnPlayerStatus=_func == null ? PlayerStatus.None : _func.Invoke();
            return returnPlayerStatus;
        }
    }
}