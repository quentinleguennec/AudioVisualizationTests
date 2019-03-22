using UnityEngine;

public class AudioPeer : Singleton<AudioPeer>
{
    [SerializeField]
    private FFTWindow fftWindowMode = FFTWindow.Blackman;

    public float[] Samples { get; private set; } = new float[GameMaster.SamplesCount];
    public float[] FrequencyBands { get; private set; } = new float[GameMaster.FrequencyBandsCount];

    private AudioSource audioSource;


    protected AudioPeer() { }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSource.GetSpectrumData(Samples, 0, fftWindowMode);
    }

    private void GetFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < GameMaster.FrequencyBandsCount; i++)
        {
            float average = 0;
            int samplesCount = (int)Mathf.Pow(2, i + 1);
            if(i == 7)
            {
                samplesCount += 2;
            }
            for (int j = 0;j  < samplesCount; j++)
            {
                average += Samples[count] * (count + 1);
                count ++;
            }

            average /= count;

            FrequencyBands[i] = average * 10f;

        }

    }
}
