using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : Singleton<AudioPeer>
{
    [SerializeField]
    private FFTWindow fftWindowMode = FFTWindow.Blackman;

    public float[] Samples { get; private set; } = new float[GameMaster.SamplesCount];
    public float[] FrequencyBands { get; private set; } = new float[GameMaster.FrequencyBandsCount];
    public float[] FrequencyBandsBuffer { get; private set; } = new float[GameMaster.FrequencyBandsCount];
    public float[] FrequencyBandsHighest { get; private set; } = new float[GameMaster.FrequencyBandsCount];

    private float amplitudeBuffer = 0f;
    private float amplitudeHighest = 0f;

    private AudioSource audioSource;


    protected AudioPeer() { }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ComputeSamples();
        ComputeFrequencyBands();
        ComputeAmplitude();
    }

    private void ComputeSamples()
    {
        audioSource.GetSpectrumData(Samples, 0, fftWindowMode);
    }

    private void ComputeAmplitude()
    {
        amplitudeBuffer = 0f;
        for (int i = 0; i < GameMaster.FrequencyBandsCount; i++)
        {
            //amplitudeBuffer += GetNormalizedBandValue(i);
            amplitudeBuffer += FrequencyBandsBuffer[i];
        }
        amplitudeBuffer /= GameMaster.FrequencyBandsCount;
        if (amplitudeBuffer > amplitudeHighest)
        {
            amplitudeHighest = amplitudeBuffer;
        }
    }

    private void ComputeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < GameMaster.FrequencyBandsCount; i++)
        {
            float average = 0;
            int samplesCount = (int)Mathf.Pow(2, i + 1);
            if (i == 7)
            {
                samplesCount += 2;
            }
            for (int j = 0; j < samplesCount; j++)
            {
                average += Samples[count] * (count + 1);
                count++;
            }

            average /= count;

            FrequencyBands[i] = average * 10f;

            // FrequencyBandsBuffer compute.
            if (FrequencyBands[i] > FrequencyBandsBuffer[i])
            {
                FrequencyBandsBuffer[i] = FrequencyBands[i];
            }
            else
            {
                float bufferDecrease = (FrequencyBandsBuffer[i] - FrequencyBands[i]) / 8;
                FrequencyBandsBuffer[i] -= bufferDecrease;
            }

            // FrequencyBandsHighest compute.
            if (FrequencyBands[i] > FrequencyBandsHighest[i])
            {
                FrequencyBandsHighest[i] = FrequencyBands[i];
            }
        }
    }

    public float GetNormalizedBandValue(int bandIndex) => FrequencyBandsBuffer[bandIndex] / (FrequencyBandsHighest[bandIndex] == 0f ? 1f : FrequencyBandsHighest[bandIndex]);
    public float GetNormalizedAmplitude() => amplitudeBuffer / (amplitudeHighest == 0f ? 1f : amplitudeHighest);
    public float GetAmplitude() => amplitudeBuffer;
      
}
