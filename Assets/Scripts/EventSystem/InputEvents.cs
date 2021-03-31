using System;
using UnityEngine;

public class InputEvents : MonoBehaviour
{
    public static InputEvents current;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        current = this;
    }
    #region TouchBeganEvents
    public Action<Vector2> OnTouchBeganEvent;
    public void TouchBeganEvent(Vector2 position)
    {
        OnTouchBeganEvent?.Invoke(position);
    }
    #endregion
    #region TouchMovedEvents
    public Action<Vector2> OnTouchMovedEvent;
    public void TouchMovedEvent(Vector2 delta)
    {
        OnTouchMovedEvent?.Invoke(delta);        
    }
    #endregion
    #region TouchEndedEvents
    public Action OnTouchEndedEvent;
    public void TouchEndedEvent()
    {        
        OnTouchEndedEvent?.Invoke();        
    }
    #endregion
    #region TouchStationaryEvents
    public Action OnTouchStationaryEvent;
    public void TouchStationaryEvent()
    {
       OnTouchStationaryEvent?.Invoke();
    }
    #endregion
    #region TouchCancelledEvents
    public Action OnTouchCancelledEvent;
    public void TouchCancelledEvent()
    {
        OnTouchCancelledEvent?.Invoke();
    }
    #endregion
    #region DoubleTouchEvent
    public Action OnDoubleTouchEvent;
    public void DoubleTouchEvent()
    {
        
        OnDoubleTouchEvent?.Invoke();
    }
    #endregion
    #region OnSlideEvent
    public Action<Vector2> OnSlideEvent;
    public void SlideEvent(Vector2 delta)
    {        
        OnSlideEvent?.Invoke(delta);        
    }
    #endregion
    #region OnTriggerWarpWall
    public Action<float> OnTriggerWarpWall;
    public void TriggerWarpWall(float _obstacle)
    {
        OnTriggerWarpWall?.Invoke(_obstacle);        
    }
    #endregion
    #region OnTeleportEvent
    public Action OnTeleportEvent;
    public void TeleportEvent()
    {
        OnTeleportEvent?.Invoke();
    }
    #endregion
}