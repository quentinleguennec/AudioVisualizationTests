using UnityEngine;

public class GameMaster : Singleton<GameMaster>
{
    public const int SamplesCount = 512;
    public const int FrequencyBandsCount = 8;

    [SerializeField]
    private GameObject sampleCubePrefab;
    [SerializeField]
    private Transform sampleCubesParent;
    [SerializeField]
    private float maxCubeHeight;

    private GameObject[] sampleCubes = new GameObject[SamplesCount];


    protected GameMaster() { }

    void Start()
    {
        for (int i = 0; i < SamplesCount; i++)
        {
            GameObject sampleCube = Instantiate(sampleCubePrefab, sampleCubesParent);
            sampleCube.name = "SampleCube" + i;
            sampleCube.transform.eulerAngles = new Vector3(0, i * (SamplesCount / 360), 0);
            sampleCube.transform.position = sampleCube.transform.forward * 100;
            sampleCubes[i] = sampleCube;
        }
    }

    void Update()
    {
        for (int i = 0; i < SamplesCount; i++)
        {
            if (sampleCubes != null)
            {
                sampleCubes[i].transform.localScale = new Vector3(1, 2 + AudioPeer.Instance.Samples[i] * maxCubeHeight, 1);
                sampleCubes[i].transform.position = new Vector3(sampleCubes[i].transform.position.x, 0.5f * (2 + AudioPeer.Instance.Samples[i] * maxCubeHeight), sampleCubes[i].transform.position.z);
            }
        }
    }
}
