using FluentSpecification.Extensions;
using FluentSpecification.Test.Models;
using FluentSpecification.Test.Specifications;
using FluentSpecification.Test.Validatons;
using FluentSpecification.Test.Validators;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FluentSpecification.Test
{
    [TestClass]
    public class PersonTest
    {
        private PersonValidator _personValidator;
        private UnityContainer _container = new UnityContainer();

        [TestInitialize]
        public void Config()
        {
            _container.RegisterType<Validator<Person>, PersonValidator>();
            _personValidator = _container.Resolve<PersonValidator>();
        }

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

        [TestMethod]
        public void CannotAcceptInvalidPerson()
        {
            //given
            var person = new Person();

            //when
            var result = _personValidator.IsValid(person);

            //then
            Assert.IsFalse(result);
            Assert.AreEqual(_personValidator.InvalidRules.Count, 2);
            Assert.IsNotNull(_personValidator.InvalidRules.FirstOrDefault(x => (PersonValidation)x == PersonValidation.InvalidName));
            Assert.IsNotNull(_personValidator.InvalidRules.FirstOrDefault(x => (PersonValidation)x == PersonValidation.InvalidEmail));
        }

        [TestMethod]
        /// Use when you want to filter specified rule in a validator
        public void CanAcceptPersonWithInvalidEmail()
        {
            //given
            var person = new Person()
            {
                FirstName = "Douglas",
                LastName = "Franco",
                Email = "invalid@email"
            };

            //when
            var result = _personValidator
                .FilterRules(typeof(PersonNameSpecification))
                .IsValid(person);

            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        /// Use when you want to add some parameters to specification
        public void CannotAcceptPersonWithMyNameAndMyEmail()
        {
            //given
            var person = new Person()
            {
                FirstName = "Douglas",
                LastName = "Franco",
                Email = "douglas.franco@dzfweb.com.br"
            };

            //when
            var result = _personValidator
                .AddParameter("MyEmail", "douglas.franco@dzfweb.com.br")
                .AddParameter("MyFirstName", "Douglas")
                .AddParameter("MyLastName", "Franco")
                .IsValid(person);

            foreach(var item in _personValidator.InvalidRules)
            {
                Console.WriteLine(item);
            }

            //then
            Assert.IsFalse(result);
            Assert.AreEqual(_personValidator.InvalidRules.Count, 2);
            Assert.IsNotNull(_personValidator.InvalidRules.FirstOrDefault(x => (PersonValidation)x == PersonValidation.InvalidName));
            Assert.IsNotNull(_personValidator.InvalidRules.FirstOrDefault(x => (PersonValidation)x == PersonValidation.InvalidEmail));
        }
    }
}