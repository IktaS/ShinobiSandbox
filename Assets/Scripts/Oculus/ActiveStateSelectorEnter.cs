using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using Oculus.Interaction;

public class ActiveStateSelectorEnter : MonoBehaviour, ISelector
{
    [SerializeField, Interface(typeof(IActiveState))]
    private MonoBehaviour _activeState;

    private IActiveState ActiveState;
    private bool _selecting = false;

    public event Action WhenSelected = delegate { };
    public event Action WhenUnselected = delegate { };
    public event Action WhenStay = delegate { };

    protected virtual void Awake()
    {
        ActiveState = _activeState as IActiveState;
    }

    protected virtual void Start()
    {
        Assert.IsNotNull(ActiveState);
    }

    protected virtual void Update()
    {
        if (_selecting != ActiveState.Active)
        {
            _selecting = ActiveState.Active;
            if (_selecting)
            {
                WhenSelected();
            }
            else
            {
                WhenUnselected();
            }
        }
        if (_selecting)
        {
            WhenStay();
        }
    }

    #region Inject

    public void InjectAllActiveStateSelector(IActiveState activeState)
    {
        InjectActiveState(activeState);
    }

    public void InjectActiveState(IActiveState activeState)
    {
        _activeState = activeState as MonoBehaviour;
        ActiveState = activeState;
    }
    #endregion
}

public interface ISelectorEnter : ISelector
{
    event Action WhenStay;
}

