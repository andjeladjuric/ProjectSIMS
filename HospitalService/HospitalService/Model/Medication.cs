using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Medication
    {
        public string Id { get; set; }
        public string MedicineName { get; set; }
        public bool IsApproved { get; set; }
        public MedicationType Type { get; set; }
        public Dictionary<string, int> Ingredients { get; set; }
        public string Format { get; set; }

        public Medication()
        {
        }

        public Medication(string id, string medicineName, bool isApproved, MedicationType type, Dictionary<string, int> ingredients, string format)
        {
            Id = id;
            MedicineName = medicineName;
            IsApproved = isApproved;
            Type = type;
            Ingredients = ingredients;
            Format = format;
        }
    }
}
