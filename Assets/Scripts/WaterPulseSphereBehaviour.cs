using UnityEngine;

public class WaterPulseSphereBehaviour : MonoBehaviour
{
    private new Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        material.SetFloat("_Scale", 0.5f * AudioPeer.Instance.GetNormalizedAmplitude());
        material.SetFloat("_EmissionStrength", 2f * AudioPeer.Instance.GetNormalizedAmplitude());
        //float rotation = Time.deltaTime * 400f * Mathf.Pow(AudioPeer.Instance.GetNormalizedAmplitude(), 4);
        float rotation = Time.deltaTime * 1.5f * Mathf.Pow(AudioPeer.Instance.GetAmplitude(), 4);
        transform.Rotate(Vector3.one * rotation);
    }
}
