using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public abstract class SceneDependentSingleton<T> : MonoBehaviour where T : Component
    {

        #region Fields
        private static T instance;

        #endregion

        #region Properties
        public static T Instance
        {
            get
            {
                if (instance == null)
                {

                    instance = FindObjectOfType<T>();
                }

                return instance;
            }
        }

        #endregion

        #region Methods
        public virtual void Awake()
        {
            if (instance == null)
            {

                instance = this as T;
                // DontDestroyOnLoad ( gameObject );
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void OnDisable()
        {

        }
        #endregion

    }
}