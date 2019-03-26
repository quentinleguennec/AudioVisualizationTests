using UnityEngine;

public class AudioBarBehaviour : MonoBehaviour
{
    private const float HeightRatio = 10f;

    [Range(0, GameMaster.FrequencyBandsCount - 1)]
    [SerializeField]
    private int bandIndex;

    private Vector3 initialScale;
    private Vector3 initialPosition;
    private new Material material;

    private float BandValue => AudioPeer.Instance.GetNormalizedBandValue(bandIndex);

    private void Start()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        transform.localScale = new Vector3(initialScale.x, initialScale.y + BandValue * HeightRatio, initialScale.z);
        transform.position = new Vector3(transform.position.x, initialPosition.y + 0.5f * BandValue * HeightRatio, transform.position.z);
        material.SetColor("_EmissionColor", Color.white * BandValue);
    }
}
