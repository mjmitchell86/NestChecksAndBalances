using System;

namespace NestChecksAndBalances.Models
{
    public class NestObject
    {
        public double humidity { get; set; }
        public string locale { get; set; }
        public string temperature_scale { get; set; }
        public bool is_using_emergency_heat { get; set; }
        public bool has_fan { get; set; }
        public string software_version { get; set; }
        public bool has_leaf { get; set; }
        public string where_id { get; set; }
        public string device_id { get; set; }
        public string name { get; set; }
        public bool can_heat { get; set; }
        public bool can_cool { get; set; }
        public double target_temperature_c { get; set; }
        public double target_temperature_f { get; set; }
        public double target_temperature_high_c { get; set; }
        public double target_temperature_high_f { get; set; }
        public double target_temperature_low_c { get; set; }
        public double target_temperature_low_f { get; set; }
        public double ambient_temperature_c { get; set; }
        public double ambient_temperature_f { get; set; }
        public double away_temperature_high_c { get; set; }
        public double away_temperature_high_f { get; set; }
        public double away_temperature_low_c { get; set; }
        public double away_temperature_low_f { get; set; }
        public double eco_temperature_high_c { get; set; }
        public double eco_temperature_high_f { get; set; }
        public double eco_temperature_low_c { get; set; }
        public double eco_temperature_low_f { get; set; }
        public bool is_locked { get; set; }
        public double locked_temp_min_c { get; set; }
        public double locked_temp_min_f { get; set; }
        public double locked_temp_max_c { get; set; }
        public double locked_temp_max_f { get; set; }
        public bool sunlight_correction_active { get; set; }
        public bool sunlight_correction_enabled { get; set; }
        public string structure_id { get; set; }
        public bool fan_timer_active { get; set; }
        public DateTime fan_timer_timeout { get; set; }
        public double fan_timer_duration { get; set; }
        public string previous_hvac_mode { get; set; }
        public string hvac_mode { get; set; }
        public string time_to_target { get; set; }
        public string time_to_target_training { get; set; }
        public string where_name { get; set; }
        public string label { get; set; }
        public string name_long { get; set; }
        public bool is_online { get; set; }
        public DateTime last_connection { get; set; }
        public string hvac_state { get; set; }
    }
}