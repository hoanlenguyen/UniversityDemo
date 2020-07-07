//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TwentyFive.Core.Entities;

//namespace UniversityDemo.BaseEntities
//{
//    public class LocateString : Dictionary<string, string>, IStructuralEquatable
//    {
//        public LocateString();
//        public LocateString(IDictionary<string, string> dictionary);

//        public KeyValuePair<string, string> this[int index] { get; set; }

//        public static LocateString Format(LocateString value, params object[] args);
//        public static LocateString From(string viValue, string enValue);
//        public static LocateString FromJson(string json);
//        public static bool IsNullOrEmpty(LocateString locateString);
//        public static LocateString Vi(string viValue);
//        public bool Equals(object other, IEqualityComparer comparer);
//        public int GetHashCode(IEqualityComparer comparer);
//        public override string ToString();

//        public static LocateString operator +(LocateString a, LocateString b);
//    }
//}
