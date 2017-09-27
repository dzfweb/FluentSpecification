using System;
using System.Runtime.CompilerServices;

namespace FluentSpecification.Attributes
{
    /// <summary>
    /// Describe the ordering for specification execution
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class SpecificationSortAttribute : Attribute
    {
        private readonly int sort_;

        /// <summary>
        /// Describe the ordering for specification execution
        /// </summary>
        /// <param name="sort">Sort index</param>
        public SpecificationSortAttribute([CallerLineNumber]int sort = 0)
        {
            sort_ = sort;
        }

        /// <summary>
        /// Sort index
        /// </summary>
        public int Sort
        {
            get { return sort_; }
        }
    }
}