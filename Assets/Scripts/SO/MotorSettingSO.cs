
using UnityEngine;
[CreateAssetMenu(fileName ="MotorSettings",menuName ="Settings/MotorSettings")]
public class MotorSettingsSO : ScriptableObject
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _steerStrenght;
    [SerializeField] private float _tiltAngle;
    [SerializeField] private float _gravity;
    [SerializeField] private float _biketiltIncrement=.09f;
    [SerializeField][Range(1,10)] private float _breakingFactor;
    public float maxSpeed => _maxSpeed;
    public float acceleration => _acceleration;
    public float steerStrenght => _steerStrenght;
    public float breakingFactor => _breakingFactor;
    public float tiltAngle => _tiltAngle;
    public float gravity => _gravity;
    public float biketiltIncrement=>_biketiltIncrement;
    
}