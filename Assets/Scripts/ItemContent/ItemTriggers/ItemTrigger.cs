using Interfaces;
using UnityEngine;

namespace ItemTriggers
{
    public abstract class ItemTrigger : MonoBehaviour,ITriggerable
    {
        protected Item Item { get; private set; }

        private void Start()
        {
            Item = GetComponent<Item>();
        }

        public void OnTriggerEnter(Collider other)
        {
            CheckTrigger(other,true);
        }

        public void OnTriggerExit(Collider other)
        {
            CheckTrigger(other,false);
        }

        protected abstract void CheckTrigger(Collider other,bool value);
    }
}
