using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // SensorResponse myDeserializedClass = JsonConvert.DeserializeObject<SensorResponse>(myJsonResponse);
    public class Result
    {
        public int ID { get; set; }
        public int Con_gas { get; set; }
        public string F_Emision { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public int Sensor_ID { get; set; }
    }

    public class SensorResponse
    {
        public string status { get; set; }
        public List<Result> results { get; set; }
    }
