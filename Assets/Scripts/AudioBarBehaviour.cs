using UnityEngine;

public class AudioBarBehaviour : MonoBehaviour
{
    private const float HeightRatio = 7f;

    [Range(0, GameMaster.FrequencyBandsCount - 1)]
    [SerializeField]
    private int bandIndex;

    private Vector3 initialScale;
    private Vector3 initialPosition;
    private Color initialEmissionColor;
    private new Renderer renderer;
    private Material material;

    private float BandValue => AudioPeer.Instance.GetNormalizedBandValue(bandIndex);

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material;
        initialScale = transform.localScale;
        initialPosition = transform.position;
        initialEmissionColor = material.GetColor("_EmissionColor");
    }

    private void Update()
    {
        transform.localScale = new Vector3(initialScale.x, initialScale.y + BandValue * HeightRatio, initialScale.z);
        transform.position = new Vector3(transform.position.x, initialPosition.y + 0.5f * BandValue * HeightRatio, transform.position.z);
        material.SetColor("_EmissionColor", initialEmissionColor * BandValue);
        //RendererExtensions.UpdateGIMaterials(renderer);
    }
}
