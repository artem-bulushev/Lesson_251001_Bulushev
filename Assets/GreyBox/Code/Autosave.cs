using StarterAssets;
using UnityEngine;

namespace Code
{
    public class Autosave : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FirstPersonController player))
            {
                Destroy(gameObject);
                Debug.Log("Autosave");
            }
        }
    }
}