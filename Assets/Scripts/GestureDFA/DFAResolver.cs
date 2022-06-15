using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFAResolver : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private RecognizeGesture _gestureEvents;
    [SerializeField] private ComboActionCaller _caller;

    [SerializeField] private AimRef aimRef;

    [Header("Combo Action")]
    [SerializeField] private ComboAction _shieldAction;
    [SerializeField] private ComboAction _activateEarthPrisonAction;
    [SerializeField] private ComboAction _aimAction;
    [SerializeField] private ComboAction _setEarthPrisonAction;
    [SerializeField] private ComboAction _shootFireAction;
    [SerializeField] private ComboAction _shootLightningAction;
    [SerializeField] private ComboAction _recoverAction;
    [SerializeField] private ComboAction _gustAction;
    [SerializeField] private ComboAction _idleAction;

    [Header("Combo Queue")]
    [SerializeField] private ComboActionQueue _comboQueue;

    private DFANode currentNode;
    private ShieldNode _shieldNode;
    private EarthPrisonNode _earthPrisonNode;
    private AimNode _aimNode;
    private FireNode _fireNode;
    private LightningNode _lightningNode;
    private GustNode _gustNode;
    private RecoverNode _recoverNode;
    private IdleNode _idleNode;
    void Start()
    {
        initializeQueue();
        initializeNodes();
        _gestureEvents.addOnRecognizeGestureListener(listenGesture);
    }

    private void initializeNodes()
    {
        var (_startPoint, _dirPoint) = aimRef.GetAim();
        _shieldNode = new ShieldNode(_shieldAction, _comboQueue);
        _earthPrisonNode = new EarthPrisonNode(_activateEarthPrisonAction, _setEarthPrisonAction, _comboQueue);
        _fireNode = new FireNode(_shootFireAction, _comboQueue);
        _lightningNode = new LightningNode(_shootLightningAction, _comboQueue);
        _gustNode = new GustNode(_gustAction, _comboQueue);
        _aimNode = new AimNode(
            _startPoint,
            _dirPoint,
            _aimAction,
            _comboQueue,
            _earthPrisonNode,
            _fireNode,
            _lightningNode,
            _gustNode
        );
        _recoverNode = new RecoverNode(_recoverAction, _comboQueue);
        _idleNode = new IdleNode(
            _shieldNode,
            _earthPrisonNode,
            _aimNode,
            _recoverNode,
            _idleAction,
            _comboQueue
        );
        currentNode = _idleNode;
    }

    private void initializeQueue()
    {
        _comboQueue.queue.Clear();
    }

    private void listenGesture(Gesture gesture)
    {
        DFANode nextNode = currentNode.exitNode(_caller, gesture);
        if (nextNode == null)
        {
            nextNode = _idleNode;
        }
        nextNode.enterNode(_caller, currentNode);
        currentNode = nextNode;
    }
}
