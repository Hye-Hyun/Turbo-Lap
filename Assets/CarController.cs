using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float acceleration = 18f;
    [SerializeField] private float reverseAcceleration = 10f;
    [SerializeField] private float maxForwardSpeed = 20f;
    [SerializeField] private float maxReverseSpeed = 8f;
    [SerializeField] private float steeringSpeed = 100f;
    [SerializeField] private float coastingDrag = 3f;

    [Header("Stability")]
    [SerializeField] private float sidewaysGrip = 8f;

    private Rigidbody carRigidbody;
    private float throttleInput;
    private float steeringInput;

    private Transform lastCheckpoint;

    private float keyboardSteering;
    private float uiSteering;

    private void Awake()
    {

        Debug.Log("Awake 실행됨"); //콘솔 확인용 로그

        if (!TryGetComponent(out carRigidbody))
        {
            carRigidbody = gameObject.AddComponent<Rigidbody>();
        }

        EnsureCollider();

        // Keep this prototype controller on a flat driving plane.
        carRigidbody.useGravity = true;
        carRigidbody.constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ;
        //carRigidbody.centerOfMass = new Vector3(0f, -0.5f, 0f);
        carRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        carRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void Update()
    {
        if (!RaceStartUI.raceStarted)
        {
            steeringInput = 0f;
            return; //시작 전까지는 입력 무시
        }

        Keyboard keyboard = Keyboard.current;

        if (keyboard != null)
        {

            if (keyboard.rKey.wasPressedThisFrame)
            {
                ResetCarRotation(); //r키를 누르면 리스폰
            }

            keyboardSteering = ReadAxis(
                keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed,
                keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed);
        }
        else
        {
            keyboardSteering = 0f;
        }

        steeringInput = keyboardSteering + uiSteering;
        steeringInput = Mathf.Clamp(steeringInput, -1f, 1f);
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate"); //콘솔 확인용

        if (!RaceStartUI.raceStarted)
        {
            carRigidbody.linearVelocity = Vector3.zero;
            return;
        }

        ApplyAcceleration();
        ApplySteering();

        ApplySidewaysGrip();

        float currentSpeed = carRigidbody.linearVelocity.magnitude * 3.6f; //km per hour

        if(currentSpeed > RaceManager.Instance.maxSpeed)
        {
            RaceManager.Instance.maxSpeed = currentSpeed;
        }
    }

    private void ApplyAcceleration()
    {

        carRigidbody.AddForce(
            transform.forward * acceleration,
            ForceMode.Acceleration);

        Debug.Log(transform.forward);
    }

    private void ApplySteering()
    {
        Debug.Log($"steeringInput={steeringInput}");

        if (Mathf.Approximately(steeringInput, 0f))
        {
            return; // 입력 없으면 회전하지 않음
        }

        float forwardSpeed = Vector3.Dot(carRigidbody.linearVelocity, transform.forward);
        float speedRatio = Mathf.Clamp01(Mathf.Abs(forwardSpeed) / 2f);

        Debug.Log($"forwardSpeed={forwardSpeed}");
        Debug.Log($"speedRatio={speedRatio}");

        if (speedRatio <= 0f)
        {
            return;
        }

        float direction = forwardSpeed >= 0f ? 1f : -1f;
        float turnAmount = steeringInput * steeringSpeed * speedRatio * direction
            * Time.fixedDeltaTime;

        carRigidbody.MoveRotation(
            carRigidbody.rotation * Quaternion.Euler(0f, turnAmount, 0f));
    }

    private void ApplySidewaysGrip()
    {
        float sidewaysSpeed = Vector3.Dot(
            carRigidbody.linearVelocity,
            transform.right);

        carRigidbody.AddForce(
            -transform.right * (sidewaysSpeed * sidewaysGrip),
            ForceMode.Acceleration);
    }

    private static float ReadAxis(bool negativePressed, bool positivePressed)
    {
        return (positivePressed ? 1f : 0f) - (negativePressed ? 1f : 0f);
    }

    private void EnsureCollider()
    {
        if (GetComponentInChildren<Collider>() != null)
        {
            return;
        }

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
        {
            BoxCollider fallbackCollider = gameObject.AddComponent<BoxCollider>();
            fallbackCollider.size = new Vector3(1.8f, 1f, 4f);
            return;
        }

        Bounds localBounds = GetLocalBounds(renderers[0].bounds);

        for (int i = 1; i < renderers.Length; i++)
        {
            Bounds rendererBounds = GetLocalBounds(renderers[i].bounds);
            localBounds.Encapsulate(rendererBounds.min);
            localBounds.Encapsulate(rendererBounds.max);
        }

        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = localBounds.center;
        boxCollider.size = localBounds.size;
    }

    private Bounds GetLocalBounds(Bounds worldBounds)
    {
        Vector3 min = worldBounds.min;
        Vector3 max = worldBounds.max;
        Bounds localBounds = new Bounds(
            transform.InverseTransformPoint(min),
            Vector3.zero);

        for (int x = 0; x <= 1; x++)
        {
            for (int y = 0; y <= 1; y++)
            {
                for (int z = 0; z <= 1; z++)
                {
                    Vector3 corner = new Vector3(
                        x == 0 ? min.x : max.x,
                        y == 0 ? min.y : max.y,
                        z == 0 ? min.z : max.z);

                    localBounds.Encapsulate(transform.InverseTransformPoint(corner));
                }
            }
        }

        return localBounds;
    }

    public void ResetCarRotation()
    {
        if (lastCheckpoint == null)
        {
            Debug.Log("저장된 체크포인트 없음");
            return;
        }

        Debug.Log($"리스폰 {lastCheckpoint.rotation.eulerAngles}");

        carRigidbody.linearVelocity = Vector3.zero;
        carRigidbody.angularVelocity = Vector3.zero;

        transform.position = lastCheckpoint.position;
        transform.rotation = lastCheckpoint.rotation;

        Debug.Log($"Respawn : {lastCheckpoint.name}");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Check Point"))
        {
            lastCheckpoint = other.transform;
            Debug.Log($"Checkpoint saved: {other.name}");
        }
    }

    public void SetSteering(float value)
    {
        uiSteering = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            RaceManager.Instance.collisionCount++;
        }
    }
}
