using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI
{
    public class UIWindowBase : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        //是否独占
        public bool isExclusive = false;

        // RectTransform组件
        public RectTransform rectTransform = null;

        //Text控件列表
        public Dictionary<string, Text> txtMap = new Dictionary<string, Text>();

        //Button控件列表
        public Dictionary<string, Button> btnMap = new Dictionary<string, Button>();

        //界面背景图
        public Image imgBackground = null;

        public virtual void Awake()
        {
            imgBackground = gameObject.GetComponent<Image>();
            RegisterWindowElemEvent();
        }
        // Use this for initialization
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        public virtual void Init()
        {
            rectTransform.offsetMin = new Vector2(0.0f, 0.0f);
            rectTransform.offsetMax = new Vector2(0.0f, 0.0f);
            rectTransform.localScale = new Vector3(1, 1, 1);
        }

        public virtual void UnInit()
        {
            txtMap.Clear();
            btnMap.Clear();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnHide();
        }

        public virtual void OnShow()
        {

        }

        public virtual void OnHide()
        {

        }

        public bool IsHide()
        {
            return !gameObject.activeSelf;
        }

        static public void OpenWindow<T>(bool bExclusive = false)
        {
            Type type = typeof(T);
            UIManager.Instance.OpenWindow(type.Name, type);
        }

        static public void CloseWindow<T>(bool bDestory = false)
        {
            Type type = typeof(T);
            UIManager.Instance.CloseWindow(type.Name, bDestory);
        }

        static public T GetWindow<T>() where T : UIWindowBase
        {
            Type type = typeof(T);
            UIWindowBase window = UIManager.Instance.GetWindow(type);
            if (window != null)
                return window as T;
            return null;
        }

        public virtual void SetImageBackground(Sprite sprite)
        {
            if (imgBackground)
                imgBackground.overrideSprite = sprite;
        }

        public virtual void SetText(string txtName, string txtContent)
        {
            Text txtComponent;
            if (txtMap.TryGetValue(txtName, out txtComponent))
            {
                txtComponent.text = txtContent;
            }
        }

        public virtual void SetText(string txtName, string txtContent, Color color)
        {
            Text txtComponent;
            if (txtMap.TryGetValue(txtName, out txtComponent))
            {
                txtComponent.text = txtContent;
                txtComponent.color = color;
            }
        }

        public virtual void SetTextColor(string txtName, Color color)
        {
            Text txtComponent;
            if (txtMap.TryGetValue(txtName, out txtComponent))
            {
                txtComponent.color = color;
            }
        }

        public virtual string GetText(string txtName)
        {
            Text txtComponent;
            if (txtMap.TryGetValue(txtName, out txtComponent))
            {
                return txtComponent.text;
            }
            return null;
        }

        public virtual void SetButtonText(GameObject obj, string txtContent, Color color, string btnTextName = "Text")
        {
            Text txtComponent = obj.transform.Find(btnTextName).GetComponent<Text>();
            txtComponent.text = txtContent;
            txtComponent.color = color;
        }

        public virtual void SetButtonText(string btnName, string txtContent, string btnTextName = "Text")
        {
            Button btnComponent;
            if (btnMap.TryGetValue(btnName, out btnComponent))
            {
                btnComponent.gameObject.transform.Find(btnTextName).GetComponent<Text>().text = txtContent;
            }
        }

        public virtual void SetButtonText(GameObject obj, string txtContent, string btnTextName = "Text")
        {
            obj.transform.Find(btnTextName).GetComponent<Text>().text = txtContent;
        }

        public virtual void SetButtonImage(GameObject obj, Sprite sprite)
        {
            obj.GetComponent<Image>().overrideSprite = sprite;
        }
        public virtual void SetButtonImage(string btnName, Sprite sprite)
        {
            Button btnComponent;
            if (btnMap.TryGetValue(btnName, out btnComponent))
                btnComponent.gameObject.GetComponent<Image>().overrideSprite = sprite;
        }
        public virtual void SetButtonColor(GameObject obj, Color color)
        {
            obj.GetComponent<Image>().color = color;
        }
        public virtual void SetButtonColor(string btnName, Color color)
        {
            Button btnComponent;
            if (btnMap.TryGetValue(btnName, out btnComponent))
                btnComponent.GetComponent<Image>().color = color;
        }
        public virtual Color GetButtonColor(GameObject obj)
        {
            return obj.GetComponent<Image>().color;
        }
        public virtual void SetElemScale(GameObject obj, float scale)
        {
            obj.transform.localScale = new Vector3(scale, scale, 1);
        }
        public virtual void SetButtonEnable(string btnName, bool isEnable)
        {
            Button btnComponent;
            if (btnMap.TryGetValue(btnName, out btnComponent))
                btnComponent.gameObject.SetActive(isEnable);
        }

        public virtual void RegisterWindowElemEvent()
        {
            RectTransform[] clildrenObjects = gameObject.GetComponentsInChildren<RectTransform>();
            foreach (RectTransform childTransform in clildrenObjects)
            {
                GameObject child = childTransform.gameObject;
                Text textComponent = child.GetComponent<Text>();
                Button buttonComponent = child.GetComponent<Button>();

                if (textComponent != null)
                {
                    txtMap[child.name] = textComponent;
                }
                if (buttonComponent != null)
                {
                    RegisterButtonClickEvent(child.name, buttonComponent);
                }

                //Debug.Log(child.name);
            }
        }

        public virtual void RegisterButtonClickEvent(string btnName, Button buttonComponent)
        {
            btnMap[btnName] = buttonComponent;
            buttonComponent.onClick.AddListener(
                delegate ()
                {
                    OnButtonClick(btnName);
                }
            );
        }

        public virtual void OnButtonClick(string strObjName)
        {
            //Debug.Log("UIWindow.OnButtonClick " + strObjName);
        }
        public virtual void OnPointerClick(PointerEventData eventData)
        {

        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {

        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
        }

        public virtual void OnDrag(PointerEventData eventData)
        {

        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {

        }
    }
}
