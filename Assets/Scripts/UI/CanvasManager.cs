﻿using System.Collections.Generic;
using extensions;
using UnityEngine;

namespace UI
{
    /**
     * Each canvas is a different menu screen.
     */
    public enum CanvasType
    {
        MainMenu,
        GameUI,
        Pause,
        Settings,
        GameOver,
    }

    /**
     * Manage which canvas is currently active.
     */
    public class CanvasManager : MonoBehaviour
    {

        private Dictionary<CanvasType, CanvasController> _canvasControllers;

        private CanvasType? _previousCanvas;

        public CanvasType startScreen = CanvasType.MainMenu;
    
        private void Awake()
        {
            _canvasControllers = GetComponentsInChildren<CanvasController>(true).ToDict(x => x.canvasType, x => x);

            SetActiveCanvas(startScreen);
        }

        private void SetActiveCanvas(CanvasType? type)
        {
            Debug.Log($"Switching to canvas: {type}");
            _canvasControllers.ForEach((k, v) =>
            {
                var isActive = v.gameObject.activeSelf;
                var isNewCanvas = k.Equals(type);
                if (isActive && !isNewCanvas) _previousCanvas = k;
                v.gameObject.SetActive(isNewCanvas);
            });
        }

        public void SwitchCanvas(CanvasType type) => SetActiveCanvas(type);

        public void GoBackToLastCanvas()
        {
            Debug.Log($"prewv: {_previousCanvas}");
            if (_previousCanvas == null) return;
        
            SetActiveCanvas(_previousCanvas ?? CanvasType.MainMenu);
        }
    }
}