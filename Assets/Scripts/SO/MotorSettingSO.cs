
using UnityEngine;
[CreateAssetMenu(fileName ="MotorSettings",menuName ="Settings/MotorSettings")]
public class MotorSettingsSO : ScriptableObject
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _steerStrenght;
    [SerializeField] private float _tiltAngle;
    [SerializeField] private float _gravity;
    [SerializeField] private float _biketiltIncrement = .09f;
    [SerializeField] private float _zTiltAngle = 45f;
    [SerializeField] private float _handleRotVal = 30f;
    [SerializeField] private float _handleRotSpeed = .15f;
    [SerializeField] private float _skidWidth = 0.062f;
    [SerializeField] private float _minSkidVelocity = 10f;
    [SerializeField] private float _tyreRotSpeed = 10000f;


    [SerializeField][Range(1, 10)] private float _breakingFactor;
    [SerializeField][Range(1, 5)] private float _maxPitchSound;
     [SerializeField][Range(0,1)] private float _minPitchSound;
    public float maxSpeed => _maxSpeed;
    public float acceleration => _acceleration;
    public float steerStrenght => _steerStrenght;
    public float breakingFactor => _breakingFactor;
    public float tiltAngle => _tiltAngle;
    public float gravity => _gravity;
    public float biketiltIncrement => _biketiltIncrement;
    public float zTiltAngle => _zTiltAngle;
    public float handleRotVal => _handleRotVal;
    public float handleRotSpeed => _handleRotSpeed;
    public float skidWidth => _skidWidth;
    public float minSkidVelocity => _minSkidVelocity;
    public float maxPitchSound=>_maxPitchSound;
    public float minPitchSound => _minPitchSound;
      public float tyreRotSpeed => _tyreRotSpeed;


    
}