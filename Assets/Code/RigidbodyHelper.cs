using UnityEngine;

namespace Code
{
    public static class RigidbodyHelper
    {
        public static Rigidbody AddOrGetComponent(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out Rigidbody rigidbody) == false)
            {
                rigidbody = gameObject.AddComponent<Rigidbody>();
            }

            return rigidbody;
        }

        public static Rigidbody GetOrAddRigidbody(this GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out Rigidbody rigidbody) == false)
            {
                rigidbody = gameObject.AddComponent<Rigidbody>();
            }

            return rigidbody;
        }
    }
}