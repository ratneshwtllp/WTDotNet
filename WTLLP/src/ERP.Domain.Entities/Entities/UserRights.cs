using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class UserRights
    {
        public long UserRightsId { get; set; }
        public int UserId { get; set; }
        public int MenuId { get; set; }

        public virtual UserDetails UserDetails { get; set; }
    }
}
