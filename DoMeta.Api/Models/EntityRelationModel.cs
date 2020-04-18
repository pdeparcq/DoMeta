﻿namespace DoMeta.Api.Models
{
    public class EntityRelationModel
    {
        public string Name { get; set; }
        public MetaTypInfoModel MetaType { get; set; }
        public int Minimum { get; set; }
        public int? Maximum { get; set; }
    }
}
