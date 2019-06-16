using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalEconomist.Entities.Models.Base
{
    [Serializable]
    public class BaseGuidDTOEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime ModifiedOn { get; private set; }
        public Boolean IsDeleted { get; set; }
    }
}
