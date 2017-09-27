using FluentSpecification.Test.Models;
using FluentSpecification.Test.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpecification.Test
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void CannotAcceptPersonWithInvalidName()
        {
            //given
            var specification = new PersonNameSpecification();
            var person = new Person();

            //when
            var result = specification.IsSatisfiedBy(person);

            //then
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CannotAcceptPersonWithInvalidEmail()
        {
            //given
            var specification = new PersonEmailSpecification();
            var person = new Person()
            {
                Email = "invalidemail@com"
            };

            //when
            var result = specification.IsSatisfiedBy(person);

            //then
            Assert.IsFalse(result);
        }
    }
}