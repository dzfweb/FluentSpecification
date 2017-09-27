using System.Collections.Generic;

namespace FluentSpecification
{
    /// <summary>
    /// Base class for all specification classess
    /// </summary>
    /// <typeparam name="T">The type of the object being validated</typeparam>
    public abstract class Specification<T>
    {
        protected IDictionary<string, object> Parameters { get; set; }

        /// <summary>
        /// Check if an entity is satified by the current specification
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Specification validation result</returns>
        public abstract bool IsSatisfiedBy(T entity);

        /// <summary>
        /// Initialize the specification class
        /// </summary>
        public Specification()
        {
            Parameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// Add parameter to be used during the validation
        /// </summary>
        /// <param name="paramameters">Parameter</param>
        public void AddParameters(IDictionary<string, object> paramameters)
        {
            if (paramameters != null)
                foreach (var parameter in paramameters)
                {
                    if (Parameters.ContainsKey(parameter.Key))
                        Parameters[parameter.Key] = parameter.Value;
                    else
                        Parameters.Add(parameter);
                }
        }
    }
}