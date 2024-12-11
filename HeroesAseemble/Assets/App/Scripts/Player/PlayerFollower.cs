using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class PlayerFollower : MonoBehaviour
    {
        public float smoothTime = 0.3f;
        public GameObject Player;

        private Vector3 velocity = Vector3.zero;

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, Player.transform.position, smoothTime * Time.deltaTime);
        }
    }
}