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
    
    public partial class AdminPersonalInfo
    {
        public int Personal_id { get; set; }
        public string Personal_no { get; set; }
        public string Personal_depart { get; set; }
        public string Personal_position { get; set; }
        public string Personal_name { get; set; }
        public string Personal_iphone { get; set; }
        public string Personal_sex { get; set; }
        public int user_id { get; set; }
    
        public virtual UserPrivileg UserPrivileg { get; set; }
    }
}
