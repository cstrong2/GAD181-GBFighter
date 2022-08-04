using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class Action
    {
        public string name;
        public int damage;
        public float reach;
        public float speed;
        public AnimationClip animationClip;
    }
}