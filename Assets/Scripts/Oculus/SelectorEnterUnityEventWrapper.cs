using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Oculus.Interaction;

public class SelectorEnterUnityEventWrapper : MonoBehaviour
{
    [SerializeField, Interface(typeof(ISelector))]
    private MonoBehaviour _selector;
    private ISelectorEnter Selector;

    [SerializeField]
    private UnityEvent _whenSelected;

    [SerializeField]
    private UnityEvent _whenUnselected;

    [SerializeField]
    private UnityEvent _whenStay;

    public UnityEvent WhenSelected => _whenSelected;
    public UnityEvent WhenUnselected => _whenUnselected;
    public UnityEvent WhenStay => _whenStay;

    protected bool _started = false;

    protected virtual void Awake()
    {
        Selector = _selector as ISelectorEnter;
    }

    protected virtual void Start()
    {
        this.BeginStart(ref _started);
        Assert.IsNotNull(Selector);
        this.EndStart(ref _started);
    }

    protected virtual void OnEnable()
    {
        if (_started)
        {
            Selector.WhenSelected += HandleSelected;
            Selector.WhenStay += HandleStay;
            Selector.WhenUnselected += HandleUnselected;
        }
    }

    protected virtual void OnDisable()
    {
        if (_started)
        {
            Selector.WhenSelected -= HandleSelected;
            Selector.WhenStay -= HandleStay;
            Selector.WhenUnselected -= HandleUnselected;
        }
    }

    private void HandleSelected()
    {
        _whenSelected.Invoke();
    }

    private void HandleUnselected()
    {
        _whenUnselected.Invoke();
    }

    private void HandleStay()
    {
        _whenStay.Invoke();
    }

    #region Inject

    public void InjectAllSelectorUnityEventWrapper(ISelectorEnter selector)
    {
        InjectSelector(selector);
    }

    public void InjectSelector(ISelectorEnter selector)
    {
        _selector = selector as MonoBehaviour;
        Selector = selector;
    }

    #endregion
}
