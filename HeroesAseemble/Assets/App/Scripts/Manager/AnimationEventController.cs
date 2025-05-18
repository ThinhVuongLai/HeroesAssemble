using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class AnimationEventController : MonoBehaviour
    {
        private List<System.Action> currentActions = new List<System.Action>();

        public void RunAnimationAction(int actionIndex)
        {
            if(currentActions==null||currentActions.Count<=0)
            {
                return;
            }

            if(actionIndex<0||actionIndex>=currentActions.Count)
            {
                return;
            }

            currentActions[actionIndex]?.Invoke();
        }

        public void AddAction(int actionIndex, System.Action animationAction)
        {
            if(actionIndex<0)
            {
                return;
            }

            if(actionIndex<currentActions.Count)
            {
                currentActions[actionIndex] = animationAction;
            }
            else
            {
                for (int i = currentActions.Count; i < actionIndex+1; i++)
                {
                    if(i==actionIndex)
                    {
                        currentActions.Add(animationAction);
                    }
                    else
                    {
                        currentActions.Add(null);
                    }
                }
            }
        }
    }
}