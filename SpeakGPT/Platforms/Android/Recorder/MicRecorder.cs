using Android.Media;

namespace SpeakGPT.Platforms.Android.Records
{
    public class MicRecorder : IRecorder
    {
        private AudioRecord _audioRecord;
        private MemoryStream _outputStream;
        private int _minBufferSize;
        public bool IsRecording { get; private set; }

        public MicRecorder()
        {
            IsRecording = false;
        }

        private void InitRecorder()
        {
            int sampleRate = 16000;  // 샘플 레이트
            ChannelIn channelConfig = ChannelIn.Mono;  // 채널
            Encoding audioFormat = Encoding.Pcm16bit; // 오디오 포맷
            _minBufferSize = AudioRecord.GetMinBufferSize(sampleRate, channelConfig, audioFormat);  // 버퍼 크기
            _audioRecord = new AudioRecord(AudioSource.Mic, sampleRate, channelConfig, audioFormat, _minBufferSize);
            _outputStream =  new MemoryStream();
        }

        public void StartRecord()
        {
            if (IsRecording)
                return;

            InitRecorder();

            IsRecording = true;
            _audioRecord.StartRecording();

            // 쓰레드 시작하여 녹음 데이터 읽기
            var thread = new Thread(new ThreadStart(ReadData));
            thread.Start();
        }
        private void ReadData()
        {
            byte[] _buffer = new byte[_minBufferSize];

            while (IsRecording)
            {
                int readSize = _audioRecord.Read(_buffer, 0, _buffer.Length);
                _outputStream.Write(_buffer, 0, readSize);
            }
        }
        public byte[] StopRecord()
        {
            if (IsRecording)
            {
                IsRecording = false;
                _audioRecord.Stop();
                _audioRecord.Release();

                return _outputStream.ToArray();
            }
            return null;
        }
        public void CancelRecord()
        {
            _outputStream.SetLength(0);
        }
    }
}
