using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SiaX
{
    public class CanvasManager : MonoBehaviour
    {
        // resize the canvas with certain ratio 
        [SerializeField] private RectTransform canvasRect;
        public float ratio;
        public float fontSize;
        public float textHeightOffset;
        public List<RectTransform> panelRects;
        public List<RectTransform> texts;
        public List<TextMeshProUGUI> textContents;
        public enum RatioRange { width, height, both };
        public RatioRange rr;

        public void SetFontSizeForContentAndMagnifying()
        {
            if (textContents.Count > 0)
            {
                textContents[0].fontSize = fontSize * 1.5f;
                textContents[1].fontSize = fontSize;
                textContents[2].fontSize = fontSize;
            }
        }
        public void ResizePanelBasedOnTextHeight()
        {
            for (int i = 0; i < texts.Count; i++)
            {
                panelRects[i].sizeDelta = new Vector2(panelRects[i].rect.width, texts[i].rect.height + textHeightOffset);
            
            }
        }
        public Vector2 SelectRatioRange(float defaultWid, float defaultHeight)
        {
            float x = canvasRect.rect.x;
            float y = canvasRect.rect.y;
            float canvasWidth = canvasRect.rect.width;
            float canvasHeight = canvasRect.rect.height;
            Rect newRect = new Rect(x, y, canvasWidth, canvasHeight);
            Vector2 newVec2 = new Vector2(defaultWid, defaultHeight);
            switch (rr)
            {
                case RatioRange.width:
                    defaultWid = newRect.width * ratio;
                    break;
                case RatioRange.height:
                    defaultHeight = newRect.height * ratio;
                    break;
                case RatioRange.both:
                    defaultHeight = newRect.height * ratio;
                    defaultWid = newRect.width * ratio;
                    break;

            }

            newVec2 = new Vector2(defaultWid, defaultHeight);
            return newVec2;
        }
        private void OnValidate()
        {
            InitResize();
        }

        private void Start()
        {
            InitResize();
        }
        private void InitResize()
        {
            canvasRect = this.GetComponent<RectTransform>();
            ResizePanelBasedOnTextHeight();

            SetFontSizeForContentAndMagnifying();
            texts.Clear();

            for (int i = 0; i < panelRects.Count; i++)
            {
                if (texts.Count < panelRects.Count)
                {

                    texts.Add(panelRects[i].GetComponentsInChildren<RectTransform>()[1]);

                }
            }

            for (int i = 0; i < panelRects.Count; i++)
            {

                Vector2 newVec2 = SelectRatioRange(panelRects[i].rect.width, panelRects[i].rect.height);
                Vector2 textVec2 = SelectRatioRange(texts[i].rect.width, texts[i].rect.height);

                panelRects[i].sizeDelta = newVec2;
                texts[i].sizeDelta = textVec2;

            }

            textContents.Clear();

            for (int i = 0; i < texts.Count; i++)
            {
                if (textContents.Count < texts.Count)
                {
                    textContents.Add(texts[i].gameObject.GetComponentInChildren<TextMeshProUGUI>());

                }
            }

        }
    }
}
