using UnityEngine;

namespace Code
{
    public sealed class ExplosionFactory
    {
        public Explosion Create(Vector3 position)
        {
            Explosion explosion = new GameObject().AddComponent<Explosion>();
            explosion.transform.position = position;
            
            return explosion;
        }
    }
}
