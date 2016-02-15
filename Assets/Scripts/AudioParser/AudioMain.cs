using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Threading;

using NAudio;
using NAudio.Wave;

/*
*
* Audio parser util
*
*/

public static class AudioMain
{
    /// <summary>
    /// Open and parse
    /// </summary>
    /// <param name="_Path"></param>
    public static void OpenFile(string _Path)
    {
        var extension = Path.GetExtension(_Path);

        NAudio.Wave.WaveStream pcm = null;

        switch (extension)
        { 
            case "wav":
                NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(new NAudio.Wave.WaveFileReader(_Path));
                break;

            case "mp3":
                pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(new NAudio.Wave.Mp3FileReader(_Path));
                break;

            default:
                throw new NotImplementedException(); // todo maybe 
        }

        var mono = ConvertToMono(pcm);

        // todo process data from mono
    }

    /// <summary>
    /// Generate wave of frequency
    /// </summary>
    /// <param name="frequency"></param>
    /// <param name="samplingRate">per sec</param>
    /// <param name="duration">secs</param>
    /// <returns></returns>
    public static float[] generateSound(float frequency, float samplingRate, float duration)
    {
        float[] pcm = new float[(int)(samplingRate * duration)];
        float increment = 2 * (float)Math.PI * frequency / samplingRate;
        float angle = 0;

        for (int i = 0; i < pcm.Length; i++)
        {
            pcm[i] = (float)Math.Sin(angle);
            angle += increment;
        }

        return pcm;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frequency"></param>
    /// <param name="samplingRate"></param>
    /// <param name="duration"></param>
    /// <param name="amplitude"></param>
    /// <returns></returns>
    public static byte[] generateSound(float frequency, float samplingRate, float duration, float amplitude)
    {
        var pcm = new byte[(int)(samplingRate * duration)];
        float increment = 2 * (float)Math.PI * frequency / samplingRate;
        float angle = 0;

        for (int i = 0; i < pcm.Length; i++)
        {
            pcm[i] = (byte)(Math.Sin(angle) * amplitude);
            angle += increment;
        }

        return pcm;
    }

    /// <summary>
    /// Play a wave
    /// </summary>
    /// <param name="_pcm">Wave</param>
    public static void PlayPCM(IWaveProvider pcm)
    {
        var device = new WaveOut();
        device.Init(pcm);

        device.Play();
        while (device.PlaybackState != PlaybackState.Stopped)
        {
            Thread.Sleep(20);
        }
        device.Stop();
    }

    /// <summary>
    /// Write to file with fixed params
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="pcm"></param>
    public static void WritePCMToFile(string filePath, byte[] pcm)
    {
        var waveFormat = new WaveFormat(8000, 16, 1); // todo check
        using (WaveFileWriter writer = new WaveFileWriter(filePath, waveFormat))
        {
            writer.Write(pcm, 0, pcm.Length);
        }
    }

    public static IWaveProvider ConvertToMono(IWaveProvider pcm)
    {
        // todo conversion

        /*
         * http://stackoverflow.com/questions/11408185/naudio-wavestream-and-multiple-channels
         * 
         * http://stackoverflow.com/questions/7207866/reducing-channel-count-when-recording-in-naudio
         * 
         * http://stackoverflow.com/questions/21798341/mixing-wavestream-with-naudio
         * 
         * http://stackoverflow.com/questions/6292905/mono-to-stereo-conversion
         * 
         * http://stackoverflow.com/questions/16285008/how-can-i-convert-stereo-pcm-samples-to-mono-samples-using-naudio
         * 
         * https://daisy-trac.cvsdude.com/urakawa-sdk/browser/trunk/csharp/audio/NAudio/Wave/WaveProviders/StereoToMonoProvider16.cs?rev=2379
         * 
         */

        //throw new NotImplementedException();

        return pcm;
    }
}
