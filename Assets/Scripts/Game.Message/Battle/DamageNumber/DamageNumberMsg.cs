using KamenFramework;
using UnityEngine;

namespace Game.Message.Battle.DamageNumber
{
    public class DamageNumberMsg : MessageModel
    {
        public int Value { get; set; }
        public bool IsCrit { get; set; }
        public bool IsHeal { get; set; }
        public Vector3 Position { get; set; }
    }
}