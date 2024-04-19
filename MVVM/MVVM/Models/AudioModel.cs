using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Models.View
{
    public class AudioModel
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan CurrentPosition { get; set; }
    }


}
