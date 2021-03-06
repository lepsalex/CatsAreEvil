using UnityEngine;

namespace Utils
{
    public class RangeFinder : MonoBehaviour
    {
        public delegate void OnEnter(Collider2D other);

        public delegate void OnExit(Collider2D other);

        public OnEnter onEnter;
        public OnExit onExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            onEnter(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            onExit(other);
        }
    }
}