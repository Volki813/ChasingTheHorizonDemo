using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*USAGE:
Simply place the script on the ScrollRect that contains the selectable children we'll be scroling to
and drag'n'drop the RectTransform of the options "container" that we'll be scrolling.*/

[RequireComponent(typeof(ScrollRect))]
[AddComponentMenu("UI/Extensions/UIScrollToSelection")]
public class UIScrollToSelection : MonoBehaviour
{

    //*** ATTRIBUTES ***//
    [Header("[ Settings ]")]
    [SerializeField]
    private ScrollType scrollDirection = ScrollType.BOTH;
    [SerializeField]
    private float scrollSpeed = 10f;

    [Header("[ Input ]")]
    [SerializeField]
    private bool cancelScrollOnInput = false;
    [SerializeField]
    private List<KeyCode> cancelScrollKeycodes = new List<KeyCode>();

    //*** PROPERTIES ***//
    // REFERENCES
    protected RectTransform LayoutListGroup
    {
        get { return TargetScrollRect != null ? TargetScrollRect.content : null; }
    }

    // SETTINGS
    protected ScrollType ScrollDirection
    {
        get { return scrollDirection; }
    }
    protected float ScrollSpeed
    {
        get { return scrollSpeed; }
    }

    // INPUT
    protected bool CancelScrollOnInput
    {
        get { return cancelScrollOnInput; }
    }
    protected List<KeyCode> CancelScrollKeycodes
    {
        get { return cancelScrollKeycodes; }
    }

    // CACHED REFERENCES
    protected RectTransform ScrollWindow { get; set; }
    protected ScrollRect TargetScrollRect { get; set; }

    // SCROLLING
    protected EventSystem CurrentEventSystem
    {
        get { return EventSystem.current; }
    }
    protected GameObject LastCheckedGameObject { get; set; }
    protected GameObject CurrentSelectedGameObject
    {
        get { return EventSystem.current.currentSelectedGameObject; }
    }
    protected RectTransform CurrentTargetRectTransform { get; set; }
    protected bool IsManualScrollingAvailable { get; set; }

    //*** METHODS - PUBLIC ***//


    //*** METHODS - PROTECTED ***//
    protected virtual void Awake()
    {
        TargetScrollRect = GetComponent<ScrollRect>();
        ScrollWindow = TargetScrollRect.GetComponent<RectTransform>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        UpdateReferences();
        CheckIfScrollingShouldBeLocked();
        ScrollRectToLevelSelection();
    }

    //*** METHODS - PRIVATE ***//
    private void UpdateReferences()
    {
        // update current selected rect transform
        if (CurrentSelectedGameObject != LastCheckedGameObject)
        {
            CurrentTargetRectTransform = (CurrentSelectedGameObject != null) ?
                CurrentSelectedGameObject.GetComponent<RectTransform>() :
                null;

            // unlock automatic scrolling
            if (CurrentSelectedGameObject != null &&
                CurrentSelectedGameObject.transform.parent == LayoutListGroup.transform)
            {
                IsManualScrollingAvailable = false;
            }
        }

        LastCheckedGameObject = CurrentSelectedGameObject;
    }

    private void CheckIfScrollingShouldBeLocked()
    {
        if (CancelScrollOnInput == false || IsManualScrollingAvailable == true)
        {
            return;
        }

        for (int i = 0; i < CancelScrollKeycodes.Count; i++)
        {
            if (Input.GetKeyDown(CancelScrollKeycodes[i]) == true)
            {
                IsManualScrollingAvailable = true;

                break;
            }
        }
    }

    private void ScrollRectToLevelSelection()
    {
        // check main references
        bool referencesAreIncorrect = (TargetScrollRect == null || LayoutListGroup == null || ScrollWindow == null);

        if (referencesAreIncorrect == true || IsManualScrollingAvailable == true)
        {
            return;
        }

        RectTransform selection = CurrentTargetRectTransform;

        // check if scrolling is possible
        if (selection == null || selection.transform.parent != LayoutListGroup.transform)
        {
            return;
        }

        // depending on selected scroll direction move the scroll rect to selection
        switch (ScrollDirection)
        {
            case ScrollType.VERTICAL:
                UpdateVerticalScrollPosition(selection);
                break;
            case ScrollType.HORIZONTAL:
                UpdateHorizontalScrollPosition(selection);
                break;
            case ScrollType.BOTH:
                UpdateVerticalScrollPosition(selection);
                UpdateHorizontalScrollPosition(selection);
                break;
        }
    }

    private void UpdateVerticalScrollPosition(RectTransform selection)
    {
        // move the current scroll rect to correct position
        float selectionPosition = -selection.anchoredPosition.y - (selection.rect.height * (1 - selection.pivot.y));

        float elementHeight = selection.rect.height;
        float maskHeight = ScrollWindow.rect.height;
        float listAnchorPosition = LayoutListGroup.anchoredPosition.y;

        // get the element offset value depending on the cursor move direction
        float offlimitsValue = GetScrollOffset(selectionPosition, listAnchorPosition, elementHeight, maskHeight);

        // move the target scroll rect
        TargetScrollRect.verticalNormalizedPosition +=
            (offlimitsValue / LayoutListGroup.rect.height) * Time.unscaledDeltaTime * scrollSpeed;
    }

    private void UpdateHorizontalScrollPosition(RectTransform selection)
    {
        // move the current scroll rect to correct position
        float selectionPosition = -selection.anchoredPosition.x - (selection.rect.width * (1 - selection.pivot.x));

        float elementWidth = selection.rect.width;
        float maskWidth = ScrollWindow.rect.width;
        float listAnchorPosition = -LayoutListGroup.anchoredPosition.x;

        // get the element offset value depending on the cursor move direction
        float offlimitsValue = -GetScrollOffset(selectionPosition, listAnchorPosition, elementWidth, maskWidth);

        // move the target scroll rect
        TargetScrollRect.horizontalNormalizedPosition +=
            (offlimitsValue / LayoutListGroup.rect.width) * Time.unscaledDeltaTime * scrollSpeed;
    }

    private float GetScrollOffset(float position, float listAnchorPosition, float targetLength, float maskLength)
    {
        if (position < listAnchorPosition + (targetLength / 2))
        {
            return (listAnchorPosition + maskLength) - (position - targetLength);
        }
        else if (position + targetLength > listAnchorPosition + maskLength)
        {
            return (listAnchorPosition + maskLength) - (position + targetLength);
        }

        return 0;
    }

    //*** ENUMS ***//
    public enum ScrollType
    {
        VERTICAL,
        HORIZONTAL,
        BOTH
    }


    /*
        public class ButtonScroller : MonoBehaviour
    {
        private PlayerInput cursorControls;

        [SerializeField] private RectTransform viewportRectTransform;
        [SerializeField] private RectTransform content;
        [SerializeField] private float transitionDuration = 0.2f;

        private TransitionHelper transitionHelper = new TransitionHelper();

        private void Update()
        {
            if (transitionHelper.InProgress)
            {
                transitionHelper.Update();
                content.transform.localPosition = transitionHelper.PosCurrent;
            }
        }

        public void HandleOnSelectChange(GameObject gObj)
        {
            float viewportTopBorderY = GetBorderTopYLocal(viewportRectTransform.gameObject);
            float viewportBottomBorderY = GetBorderBottomYLocal(viewportRectTransform.gameObject);

            // top
            float targetTopBorderY = GetBorderBottomYRelative(gObj);
            float targetTopYWithViewportOffset = targetTopBorderY + viewportTopBorderY;

            // bottom
            float targetBottomBorderY = GetBorderBottomYRelative(gObj);
            float targetBottomYWithViewportOffset = targetBottomBorderY - viewportBottomBorderY;

            // top difference 
            float topDiff = targetTopYWithViewportOffset - viewportTopBorderY;
            if (topDiff > 0f)
            {
                MoveContentObjectYByAmount((topDiff * 100f) + GetGridLayoutGroup().padding.top);
            }

            // bottom difference
            float bottomDiff = targetBottomYWithViewportOffset - viewportBottomBorderY;
            if(bottomDiff < 0f)
            {
                MoveContentObjectYByAmount((bottomDiff * 100f) - GetGridLayoutGroup().padding.bottom);
            }
        }

        private float GetBorderTopYLocal(GameObject gObj)
        {
            Vector3 pos = gObj.transform.localPosition / 100f;
            return pos.y;
        }

        private float GetBorderBottomYLocal(GameObject gObj)
        {
            Vector3 rectSize = gObj.GetComponent<RectTransform>().rect.size * 0.01f;
            Vector3 pos = gObj.transform.localPosition / 100f;
            pos.y -= rectSize.y;

            return pos.y;
        }

        private float GetBorderTopYRelative(GameObject gObj)
        {
            float contentY = content.transform.localPosition.y / 100f;
            float targetBorderUpYLocal = GetBorderTopYLocal(gObj);
            float targetBorderUpYRelative = targetBorderUpYLocal + contentY;

            return targetBorderUpYRelative;
        }

        private float GetBorderBottomYRelative(GameObject gObj)
        {
            float contentY = content.transform.localPosition.y / 100f;
            float targetBorderBottomYLocal = GetBorderBottomYLocal(gObj);
            float targetBorderBottomYRelative = targetBorderBottomYLocal + contentY;

            return targetBorderBottomYRelative;
        }

        private void MoveContentObjectYByAmount(float amount)
        {
            Vector2 posScrollFrom = content.transform.localPosition;
            Vector2 posScrollTo = posScrollFrom;
            posScrollTo.y -= amount;

            transitionHelper.TransitionPositionFromTo(posScrollFrom, posScrollTo, transitionDuration);
        }

        private GridLayoutGroup GetGridLayoutGroup()
        {
            GridLayoutGroup gridLayoutGroup = content.GetComponent<GridLayoutGroup>();
            return gridLayoutGroup;
        }

        private class TransitionHelper
        {
            private float duration = 0f;
            private float timeElapsed = 0f;
            private float progress = 0f;

            private bool inProgress = false;

            private Vector2 posCurrent;
            private Vector2 posFrom;
            private Vector2 posTo;

            public bool InProgress
            {
                get => inProgress;
            }

            public Vector2 PosCurrent
            {
                get => posCurrent;
            }

            public void Update()
            {
                Tick();

                CalculatePosition();
            }

            public void Clear()
            {
                duration = 0f;
                timeElapsed = 0f;
                progress = 0f;
                inProgress = false;
            }

            public void TransitionPositionFromTo(Vector2 posFrom, Vector2 posTo, float duration)
            {
                Clear();

                this.posFrom = posFrom;
                this.posTo = posTo;
                this.duration = duration;
                inProgress = true;
            }

            private void CalculatePosition()
            {
                posCurrent.x = Mathf.Lerp(posFrom.x, posTo.x, progress);
                posCurrent.y = Mathf.Lerp(posFrom.y, posTo.y, progress);
            }

            private void Tick()
            {
                if (!inProgress) return;

                timeElapsed += Time.deltaTime;
                progress = timeElapsed / duration;
                if (progress > 1f) progress = 1f;
                if (progress >= 1f) TransitionComplete();
            }

            private void TransitionComplete()
            {
                inProgress = false;
            }
        }

        //[SerializeField] private float lerpTime;
        //[SerializeField] private GameObject scrollContainer;
        //private ScrollRect scrollRect;
        //private Button[] buttons;
        //private int index;
        //private float verticalPosition;
        //private bool up;
        //private bool down;

        //private void Awake()
        //{
        //    scrollRect = GetComponent<ScrollRect>();
        //    buttons = scrollContainer.GetComponentsInChildren<Button>();
        //    cursorControls = GetComponent<PlayerInput>();
        //}

        //private void Start()
        //{
        //    buttons[index].Select();
        //    verticalPosition = 1f - ((float)index / (buttons.Length - 1));
        //}

        //private void Update()
        //{
        //    up = cursorControls.actions["Navigate"].ReadValue<Vector2>().y > 0.001 && cursorControls.actions["Navigate"].WasPressedThisFrame();
        //    down = cursorControls.actions["Navigate"].ReadValue<Vector2>().y < -0.001 && cursorControls.actions["Navigate"].WasPressedThisFrame();

        //    if (up ^ down) 
        //    {
        //        if (up)
        //        {
        //            index = Mathf.Clamp(index - 1, 0, buttons.Length - 1);
        //        }
        //        else
        //        {
        //            index = Mathf.Clamp(index + 1, 0, buttons.Length - 1);
        //        }
        //        buttons[index].Select();
        //        verticalPosition = 1f - ((float)index / (buttons.Length - 1));
        //    }
        //    scrollRect.verticalNormalizedPosition = Mathf.Lerp(scrollRect.verticalNormalizedPosition, verticalPosition, Time.deltaTime / lerpTime);
        //}
    */
}
