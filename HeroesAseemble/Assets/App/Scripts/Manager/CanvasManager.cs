using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CanvasManager : SceneDependentSingleton<CanvasManager>
    {
        [SerializeField] private List<CanvasData> canvasDatas = new List<CanvasData>();

        private RectTransform _rectTransform;

        public RectTransform UIRectTransform
        {
            get
            {
                if(_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        public RectTransform GetCanvasRectTransform(CanvasType canvasType)
        {
            CanvasData canvasData = canvasDatas.Find(canvasData => canvasData.canvasType == canvasType);

            if(canvasData == null)
            {
                return null;
            }

            return canvasData.canvasRectTRansform;
        }
    }

    [System.Serializable]
    public enum CanvasType
    {
        HealthBar,
        Background,
        Menu,
        Popup,
        AlwaysBottom,
    }

    [System.Serializable]
    public class CanvasData
    {
        public CanvasType canvasType;
        public RectTransform canvasRectTRansform;
    }
}