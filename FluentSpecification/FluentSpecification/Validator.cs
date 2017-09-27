using FluentSpecification.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentSpecification
{
    internal class Validator
    {
    } /// <summary>

    /// Base Class for entity validator classess
    /// </summary>
    /// <typeparam name="T">The type of the object being validated</typeparam>
    public abstract class Validator<T>
    {
        /// <summary>
        /// List of invalid rules
        /// </summary>
        public List<Enum> InvalidRules;

        /// <summary>
        /// List of specification to be validated
        /// </summary>
        public IList<Specification<T>> Rules;

        /// <summary>
        /// List of optional parameters to be used during validation
        /// </summary>
        public IDictionary<string, object> Parameters { get; set; }

        /// <summary>
        /// List of specification to be validated
        /// </summary>
        /// <param name="rules"></param>
        public Validator(params Specification<T>[] rules)
        {
            Rules = rules;
        }

        /// <summary>
        /// Check if all specification is valid
        /// </summary>
        /// <param name="entity">Entity to be validated by all specifications</param>
        /// <returns></returns>
        public bool IsValid(T entity) =>
            entity == null ? false : BrokenRules(entity).Count() == 0;

        private IEnumerable<Enum> BrokenRules(T entity)
        {
            InvalidRules = Rules
                            .SortSpecifications()
                            .Where(rule => !ExecuteRuleSuccessfuly(rule, entity))
                            .Select(rule => rule.GetEnumValidation()).ToList();

            return InvalidRules;
        }

        /// <summary>
        /// Execute rule and add parameters into specification
        /// </summary>
        /// <param name="rule">Current specification</param>
        /// <param name="entity">Entity to be validated</param>
        /// <returns></returns>
        private bool ExecuteRuleSuccessfuly(Specification<T> rule, T entity)
        {
            rule.AddParameters(Parameters);
            return rule.IsSatisfiedBy(entity);
        }
    }
}