using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityDemo.BaseEntities
{
    public interface IStructuralEquatable
    {
        //
        // Summary:
        //     Determines whether an object is structurally equal to the current instance.
        //
        // Parameters:
        //   other:
        //     The object to compare with the current instance.
        //
        //   comparer:
        //     An object that determines whether the current instance and other are equal.
        //
        // Returns:
        //     true if the two objects are equal; otherwise, false.
        bool Equals(object other, IEqualityComparer comparer);
        //
        // Summary:
        //     Returns a hash code for the current instance.
        //
        // Parameters:
        //   comparer:
        //     An object that computes the hash code of the current object.
        //
        // Returns:
        //     The hash code for the current instance.
        int GetHashCode(IEqualityComparer comparer);
    }
}
