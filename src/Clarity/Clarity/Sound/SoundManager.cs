using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Sound
{
    class SoundData : IDisposable
    {
        public void Dispose()
        {
            
        }
    }

    class SoundManager : BaseClarityFactroy<SoundManager, SoundData>
    {
    }
}
