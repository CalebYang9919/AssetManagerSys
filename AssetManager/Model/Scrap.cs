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
    
    public partial class Scrap
    {
        public int scr_id { get; set; }
        public string scr_name { get; set; }
        public System.DateTime scr_time { get; set; }
        public int scr_type { get; set; }
        public string scr_reason { get; set; }
        public int scr_wareid { get; set; }
    
        public virtual ScrapType ScrapType { get; set; }
        public virtual Warehous Warehous { get; set; }
    }
}
