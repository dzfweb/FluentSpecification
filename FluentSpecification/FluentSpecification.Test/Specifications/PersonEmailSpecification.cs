using FluentSpecification.Attributes;
using FluentSpecification.Extensions;
using FluentSpecification.Test.Models;
using FluentSpecification.Test.Validatons;
using System.ComponentModel.DataAnnotations;

namespace FluentSpecification.Test.Specifications
{
    [SpecificationError(PersonValidation.InvalidEmail)]
    public class PersonEmailSpecification : Specification<Person>
    {
        public override bool IsSatisfiedBy(Person entity) =>
            !string.IsNullOrEmpty(entity.Email) && 
            new EmailAddressAttribute().IsValid(entity.Email) &&
            entity.Email != (string)Parameters.GetValueOrDefault("MyEmail", "");
    }
}