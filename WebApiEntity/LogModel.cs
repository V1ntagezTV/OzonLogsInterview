using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiEntity
{
    public class LogModel
    {
        [Key] public int Id { get; set; }
        public string Content { get; set; }
        public LogLevel Level { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
    }
}