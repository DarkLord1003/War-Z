using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKController : MonoBehaviour
{
    [Header("Look At Object")]
    [SerializeField] private Transform _lootAt;
    [SerializeField] [Range(0, 1)] private float _lookAtWeight;

    [Header("Right Hand Grip")]
    [SerializeField] private Transform _rightHand;
    [SerializeField] [Range(0, 1)] private float _rightIKWeight;
    [SerializeField] private Transform _rightHint;
    [SerializeField] [Range(0, 1)] private float _rightHintWeight;

    [Header("Left Hand Grip")]
    [SerializeField] private Transform _leftHand;
    [SerializeField] [Range(0, 1)] private float _leftIKWeight;
    [SerializeField] private Transform _leftHint;
    [SerializeField] [Range(0, 1)] private float _leftHintWeight;

    private Animator _animator;
    private static IKController _ikContoller;

    public static IKController Instance =>_ikContoller;

    private void Awake()
    {
        if(_ikContoller == null)
        {
            _ikContoller = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layer)
    {
        if (_animator == null)
            return;

        if (_lootAt)
        {
            _animator.SetLookAtPosition(_lootAt.position);
            _animator.SetLookAtWeight(_lookAtWeight);
        }

        if (_rightHand)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _rightIKWeight);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, _rightIKWeight);
            _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHand.position);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHand.rotation);
        }

        if (_rightHint)
        {
            _animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, _rightHintWeight);
            _animator.SetIKHintPosition(AvatarIKHint.RightElbow, _rightHint.position);
        }

        if (_leftHand)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _leftIKWeight);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, _leftIKWeight);
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHand.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHand.rotation);
        }

        if (_leftHint)
        {
            _animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, _leftHintWeight);
            _animator.SetIKHintPosition(AvatarIKHint.LeftElbow, _leftHint.position);
        }
    }


}
