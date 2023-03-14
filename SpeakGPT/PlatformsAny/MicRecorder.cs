using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakGPT.PlatformsAny
{
    internal class MicRecorder : IRecorder
    {
        public bool IsRecording { get; private set; }

        public void StartRecord()
        {
            throw new NotImplementedException();
        }

        public byte[] StopRecord()
        {
            throw new NotImplementedException();
        }
        public void CancelRecord()
        {
            throw new NotImplementedException();
        }
    }
}
