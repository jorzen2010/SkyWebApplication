using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyWebApplication.Models
{
    public class Dictionary
    {
        public int ID { get; set; }
        public string DictionaryName { get; set; }
        public string DictionaryInfo { get; set; }
        public int DictionaryParentID { get; set; }
        public bool DictionaryStatus { get; set; }
        public int DictionarySort { get; set; }

        [NotMapped]
        public string DictionaryParentName { get; set; }
        [NotMapped]
        public List<Dictionary> ChildDictionary { get; set; }
    }
}