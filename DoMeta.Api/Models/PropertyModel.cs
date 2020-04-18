using System;

namespace DoMeta.Api.Models
{
    public class PropertyModel
    {
        public string Name { get; set; }
        public string SystemType { get; set; }
        public MetaTypInfoModel MetaType { get; set; }
    }
}
