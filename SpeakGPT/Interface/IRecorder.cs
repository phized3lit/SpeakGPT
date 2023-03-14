using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakGPT
{
    public interface IRecorder
    {
        void StartRecord();
        byte[] StopRecord();
        void CancelRecord();
    }
}
