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
    
    public partial class ScrapType
    {
        public ScrapType()
        {
            this.Scrap = new HashSet<Scrap>();
        }
    
        public int scrap_id { get; set; }
        public string scrap_no { get; set; }
        public string scrap_name { get; set; }
        public int scrap_state { get; set; }
    
        public virtual ICollection<Scrap> Scrap { get; set; }
    }
}
