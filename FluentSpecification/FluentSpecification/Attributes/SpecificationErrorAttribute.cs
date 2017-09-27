using System;

namespace FluentSpecification.Attributes
{
    /// <summary>
    /// Specification error attribute to be used into specification to describe the validation fails as enum
    /// </summary>
    public class SpecificationErrorAttribute : Attribute
    {
        /// <summary>
        /// The current enum
        /// </summary>
        public Enum error { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type"></param>
        public SpecificationErrorAttribute(object type)
        {
            error = (Enum)type;
        }
    }
}