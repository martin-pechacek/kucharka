//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Semestralka.DatabaseModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recipe_Ingredient
    {
        public short id_recipe { get; set; }
        public short id_ingredient { get; set; }
        public int amount { get; set; }
    
        public virtual Ingredient Ingredient { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}