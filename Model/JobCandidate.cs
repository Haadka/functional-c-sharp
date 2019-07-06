using System;
using System.Collections.Generic;

namespace ListProcessing
{
    internal class JobCandidate : IComparable
    {
        public Guid Id { get; set; }
        public long Value { get; set; }
        public int Index { get; set; }
        public string Guid { get; set; }
        public bool Isactive { get; set; }
        public string Balance { get; set; }
        public string Picture { get; set; }
        public int Age { get; set; }
        public string EyeColor { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string About { get; set; }
        public string Registered { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<string> Tags { get; set; }
        public List<Reference> References { get; set; }
        public string Greeting { get; set; }
        public string FavoriteFruit { get; set; }


        public override bool Equals(object obj)
        {
            return obj is JobCandidate other &&
                   Id.Equals(other.Id) &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Value);
        }

        public int CompareTo(object obj)
        {
            return this.Index.CompareTo(((Employee)obj).Index);
        }

        public void ComputeFibonacci()
        {
            int a = 0;
            int b = 1;
            for (int i = 0; i < Age; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
            }
            Value = a;
        }
    }
}
