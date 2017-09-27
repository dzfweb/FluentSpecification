using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentSpecification.Extensions
{
    /// <summary>
    /// Extensions to be used with Validator classes
    /// </summary>
    public static class ValidatorExtension
    {
        /// <summary>
        /// Filter rules inside the current specification
        /// </summary>
        /// <typeparam name="T">The type of the object being validated</typeparam>
        /// <param name="validator">The current validator</param>
        /// <param name="rules">The rules to be used during this validation</param>
        /// <returns>The current validator filtered with only specification passed</returns>
        public static Validator<T> FilterRules<T>(this Validator<T> validator, params Type[] rules)
        {
            validator.Rules = validator.Rules.Where(p => rules.Contains(p.GetType())).ToList();

            return validator;
        }

        /// <summary>
        /// Add parameter to be used during the validation inside the specification
        /// </summary>
        /// <typeparam name="T">The type of the object being validated</typeparam>
        /// <param name="validator">The current validator</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>The current validator with parameters added</returns>
        public static Validator<T> AddParameter<T>(this Validator<T> validator, string parameterName, object parameterValue)
        {
            if (validator.Parameters == null)
                validator.Parameters = new Dictionary<string, object>();

            if (validator.Parameters.ContainsKey(parameterName))
                validator.Parameters[parameterName] = parameterValue;
            else
                validator.Parameters.Add(new KeyValuePair<string, object>(parameterName, parameterValue));

            return validator;
        }
    }
}