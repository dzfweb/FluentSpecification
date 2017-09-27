using FluentSpecification.Test.Models;
using FluentSpecification.Test.Specifications;

namespace FluentSpecification.Test.Validators
{
    public class PersonValidator : Validator<Person>
    {
        public PersonValidator(PersonNameSpecification personNameSpecification,
                               PersonEmailSpecification personEmailSpecification) 
            : base(personNameSpecification,
                personEmailSpecification)
        {
        }
    }
}