using FluentSpecification.Attributes;
using FluentSpecification.Extensions;
using FluentSpecification.Test.Models;
using FluentSpecification.Test.Validatons;

namespace FluentSpecification.Test.Specifications
{
    [SpecificationError(PersonValidation.InvalidName)]
    public class PersonNameSpecification : Specification<Person>
    {
        public override bool IsSatisfiedBy(Person entity) =>
            !string.IsNullOrEmpty(entity.FirstName) &&
            !string.IsNullOrEmpty(entity.LastName) &&
            entity.FirstName != (string)Parameters.GetValueOrDefault("MyFirstName", "") &&
            entity.LastName != (string)Parameters.GetValueOrDefault("MyLastName", "");
    }
}