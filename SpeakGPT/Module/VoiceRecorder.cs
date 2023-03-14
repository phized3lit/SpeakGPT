using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using SpeakGPT.MVVM;
#if ANDROID
using SpeakGPT.Platforms.Android.Records;
#else
using SpeakGPT.PlatformsAny;
#endif

namespace SpeakGPT.Module
{
    internal class VoiceRecorder : BaseModel
    {
        private IRecorder _deviceRecorder;
        private bool _isRecording;
        public bool IsRecording { get { return _isRecording; } private set { _isRecording = value; NotifyPropertyChanged(); } }
        internal VoiceRecorder()
        {
            _deviceRecorder = new MicRecorder();
        }
        //internal byte[] Record()
        //{
        //    byte[] result = null;
        //
        //    if (IsRecording == false)
        //        StartRecord();
        //    else
        //        result = EndRecord();
        //
        //    return result;
        //}
        internal void StartRecord()
        {
            IsRecording = true;
            _deviceRecorder.StartRecord();
        }
        internal void CancelRecord()
        {
            IsRecording = false;
            _deviceRecorder.CancelRecord();
        }
        internal byte[] EndRecord()
        {
            byte[] audioBytes = _deviceRecorder.StopRecord();
            audioBytes = ToWav(audioBytes);
            IsRecording = false;

            return audioBytes;
        }

        private byte[] ToWav(byte[] audioData)
        {
            // WAV 파일의 형식 설정
            var format = new WaveFormat(16000, 16, 1);

            // WAV 파일로 변환하기 위한 MemoryStream 생성
            var outputStream = new MemoryStream();

            // WAV 파일로 변환하는 WaveFileWriter 생성
            using (var writer = new WaveFileWriter(outputStream, format))
            {
                // 변환할 오디오 데이터를 읽어와서 WAV 파일에 쓰기
                writer.Write(audioData, 0, audioData.Length);
            }

            // WAV 파일로 변환된 데이터를 바이트 배열로 변환
            byte[] wavData = outputStream.ToArray();

            return wavData;
        }
    }
}
