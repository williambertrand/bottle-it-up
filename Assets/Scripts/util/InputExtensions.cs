using System;
using JetBrains.Annotations;
using UnityEngine.InputSystem;

namespace extensions
{
    public static class InputExtensions
    {

        public static void AddListeners(
            this InputAction inputAction,
        [CanBeNull] Action started = null,
        [CanBeNull] Action performed = null,
        [CanBeNull] Action canceled = null)
        {
            if (started != null) inputAction.OnStarted(started);
            if (performed != null) inputAction.OnPerformed(performed);
            if (canceled != null) inputAction.OnCanceled(canceled);
        }

        public static void AddListeners<T>(
            this InputAction inputAction,
            [CanBeNull] Action<T> started = null,
            [CanBeNull] Action<T> performed = null,
            [CanBeNull] Action<T> canceled = null)
            where T : struct
        {
            if (started != null) inputAction.OnStarted(started);
            if (performed != null) inputAction.OnPerformed(performed);
            if (canceled != null) inputAction.OnCanceled(canceled);
        }

        public static void OnStarted(this InputAction inputAction, Action started)
        {
            inputAction.started += c => started.Invoke();
        }

        public static void OnPerformed(this InputAction inputAction, Action performed)
        {
            inputAction.performed += c => performed.Invoke();
        }

        public static void OnCanceled(this InputAction inputAction, Action canceled)
        {
            inputAction.canceled += c => canceled.Invoke();
        }
        
        public static void OnStarted<T>(this InputAction inputAction, Action<T> started)
            where T : struct
        {
            inputAction.started += c => started.Invoke(c.ReadValue<T>());
        }

        public static void OnPerformed<T>(this InputAction inputAction, Action<T> performed)
            where T : struct
        {
            inputAction.performed += c => performed.Invoke(c.ReadValue<T>());
        }

        public static void OnCanceled<T>(this InputAction inputAction, Action<T> canceled)
            where T : struct
        {
            inputAction.canceled += c => canceled.Invoke(c.ReadValue<T>());
        }
    }
}