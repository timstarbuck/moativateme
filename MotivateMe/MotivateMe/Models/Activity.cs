using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotivateMe.Models
{
    public class Activity
    {
        public Activity()
        {
            Timestamp = DateTime.Now;
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public double MetricOne { get; set; }

        public double MetricTwo { get; set; }

        public DateTime Timestamp { get; set; }

        public TimeSpan Time
        {
            get
            {
                return Timestamp.TimeOfDay;
            }

            set
            {
                Timestamp = Timestamp.Add(value);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Name: {0}", Name).AppendLine();
            sb.AppendFormat("Type: {0}", Type).AppendLine();
            sb.AppendFormat("MetricOne: {0}", MetricOne).AppendLine();
            sb.AppendFormat("MetricTwo: {0}", MetricTwo).AppendLine();
            sb.AppendFormat("Timestamp: {0}", Timestamp).AppendLine();
            return sb.ToString();
        }

        public bool canSave()
        {

            return !String.IsNullOrEmpty(this.Name) &&
                    !String.IsNullOrEmpty(this.Type) &&
                    this.MetricOne > 0 &&
                    this.MetricTwo > 0 &&
                    this.Timestamp != null && this.Timestamp != DateTime.MinValue;

        }
    }
}
