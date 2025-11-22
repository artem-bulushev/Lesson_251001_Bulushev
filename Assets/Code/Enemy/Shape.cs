using System;
using UnityEngine;

namespace Code
{
    public class Shape : MonoBehaviour
    {
        private Action<Shape> _killAction;
        public void Init(Action<Shape> killAction)
        { 
            _killAction = killAction; 
        }

        private void OnCollisionEnter(Collision col)
        {
            if(col.transform.CompareTag("Ground"))
            {
                _killAction(this);
            }
        }
    }
}
    
