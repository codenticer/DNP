using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Models.Shopping
{
    public class Files
    {
        public List<object> PhotoList { get; set; }
        public List<object> ThumbnailList { get; set; }
        public List<object> HighResolutionPhotoList { get; set; }
        public List<object> PresentationList { get; set; }
        public List<object> AdditionalPhotoList { get; set; }
        public List<DocumentList> DocumentList { get; set; }
        public List<object> ParametersImages { get; set; }
    }
}
