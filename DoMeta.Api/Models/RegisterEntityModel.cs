using System;

namespace DoMeta.Api.Models
{
    public class RegisterEntityModel
    {
        public Guid BoundedContextId { get; set; }
        public string Name { get; set; }
    }
}
