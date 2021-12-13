using System;
using System.Collections;
using Spells.Parts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Button))]
    public class PartController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//разбить на более мелкие скрипты по функционалу
    {
        [Space]
        [Header("Resources")]
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Image icon;
        [SerializeField] private Button holder;

        [Space] 
        [Header("Colors")] 
        [SerializeField] private Color pickedColor;
        [SerializeField] private Color unpickedColor;
        
        [Space]
        [Header("Moving")]
        [SerializeField] private float delay = 0.2f;
        [SerializeField] private float updateTime = 1;
        [SerializeField] private float hoverHeight = 120;
        [SerializeField] private float notHoverHeight = 45;
        [SerializeField] private float width = 180;
        
        public event Action<PartController> OnActivateButton;
        private RectTransform _rectTransform;
        private bool sizeChanging = false;
        private bool isPointerInside = false;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            SetHeight(false);
            SetBlockDisabled();
        }

        public void SetResources(SpellPartInfo spellPartInfo)
        {
            title.text = spellPartInfo.title;
            description.text = spellPartInfo.description;
            icon.sprite = spellPartInfo.icon;
        }

        public void SetBlockEnabled()
        {
            ColorBlock newColorBlock = holder.colors;
            newColorBlock.disabledColor = pickedColor;
            newColorBlock.selectedColor = pickedColor;
            newColorBlock.normalColor= pickedColor;
            newColorBlock.pressedColor = pickedColor + Color.blue;
            newColorBlock.highlightedColor = pickedColor;
            holder.colors = newColorBlock;
        }
        
        public void SetBlockDisabled()
        {
            ColorBlock newColorBlock = holder.colors;
            newColorBlock.disabledColor = unpickedColor;
            newColorBlock.selectedColor = unpickedColor;
            newColorBlock.normalColor = unpickedColor;
            newColorBlock.pressedColor = unpickedColor + Color.blue;
            newColorBlock.highlightedColor = unpickedColor;
            holder.colors = newColorBlock;
        }

        public void ButtonClicked()
        {
            OnActivateButton?.Invoke(this);
        }
        
        private void SetHeight(bool isOnHover)
        {
            if (!sizeChanging && (isOnHover == isPointerInside))
                if (isOnHover)
                    StartCoroutine(ChangeHeight(true));
                else 
                    StartCoroutine(ChangeHeight(false));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isPointerInside = true;
            StartCoroutine(DelayedStart<bool>(delay, SetHeight, isPointerInside));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPointerInside = false;
            SetHeight(isPointerInside);
        }

        IEnumerator DelayedStart<T>(float delayTime, Action<T> functionToDelay, T functionParameter)
        {
            yield return new WaitForSeconds(delayTime);
            functionToDelay(functionParameter);
        }

        IEnumerator ChangeHeight(bool isHovering)
        {
            sizeChanging = true;
            var timer = 0f;
            var startSizeDelta = _rectTransform.sizeDelta;
            var endSizeDelta = new Vector2();

            if (isHovering)
                endSizeDelta = new Vector2(width, hoverHeight);
            else
                endSizeDelta = new Vector2(width, notHoverHeight);

            GetComponentInParent<LayoutElement>().preferredHeight = endSizeDelta.y;//вынести в отдедльный класс

            while (timer < updateTime)
            {
                _rectTransform.sizeDelta = Vector2.Lerp(startSizeDelta, endSizeDelta, timer / updateTime);
                timer += Time.deltaTime;
                yield return null;
            }

            _rectTransform.sizeDelta = endSizeDelta;
            sizeChanging = false;
            if (isHovering != isPointerInside)
                yield return StartCoroutine(ChangeHeight(!isHovering));
        }

        private void OnDestroy()
        {
            OnActivateButton = null;
        }
    }
}
