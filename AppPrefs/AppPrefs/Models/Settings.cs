using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppPrefs.Models
{
    [Table ("settings")]
    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string strSettings { get; set; }
    }
}
