//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserPrivileg
    {
        public UserPrivileg()
        {
            this.AdminPersonalInfo = new HashSet<AdminPersonalInfo>();
        }
    
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_pwd { get; set; }
        public int power { get; set; }
        public string Per { get; set; }

        public virtual ICollection<AdminPersonalInfo> AdminPersonalInfo { get; set; }
    }
}
