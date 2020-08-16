using System;
using UnityEngine;

namespace baseclasses
{
    
    public class MonoBehaviorWithInputs : MonoBehaviour
    {

        private PlayerActions _inputActions;
        protected PlayerActions InputActions => _inputActions ?? (_inputActions = new PlayerActions());
        private void OnEnable() => InputActions.Enable();

        private void OnDisable() => InputActions.Disable();
    }
}