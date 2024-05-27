using UnityEngine;

public class DominoController : MonoBehaviour
{
    public float fallThreshold = 30f;
    public float fallSpeedMultiplier = 1f;
    [SerializeField] private float maxVelocity = 20f;
    private Renderer dominoRenderer;
    private Rigidbody dominoRigidbody;
    private bool hasFallen = false;

    private void Start()
    {
        dominoRenderer = GetComponent<Renderer>();
        dominoRigidbody = GetComponent<Rigidbody>();
    }

    //private void Update()
    //{
    //    if (!hasFallen && transform.rotation.eulerAngles.x > fallThreshold)
    //    {
    //        hasFallen = true;
    //        TurnOffEmissive();
    //        dominoRigidbody.velocity *= fallSpeedMultiplier;
    //    }
    //}

    private void Update()
    {
        if (!hasFallen && transform.rotation.eulerAngles.x > fallThreshold)
        {
            hasFallen = true;
            TurnOffEmissive();
            dominoRigidbody.velocity *= fallSpeedMultiplier;

            // Limit the maximum velocity magnitude
           
            dominoRigidbody.velocity = Vector3.ClampMagnitude(dominoRigidbody.velocity, maxVelocity);
        }
    }

    private void TurnOffEmissive()
    {
        Material material = dominoRenderer.material;
        material.SetColor("_EmissiveColor", Color.black);
        material.SetColor("_EmissiveColorLDR", Color.black);
        material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;
    }
}