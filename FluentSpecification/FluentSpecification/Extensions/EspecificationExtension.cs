using FluentSpecification.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentSpecification.Extensions
{
    public static class EspecificationExtension
    {
        /// <summary>
        /// Get the specification enumerator validation
        /// </summary>
        /// <typeparam name="T">The type of the object being validated</typeparam>
        /// <param name="specification">The current specification</param>
        /// <returns>The validation enum</returns>
        public static Enum GetEnumValidation<T>(this Specification<T> specification)
        {
            var errorAttribute = ((SpecificationErrorAttribute)(specification.GetType().GetCustomAttributes(typeof(SpecificationErrorAttribute), true)).FirstOrDefault());
            if (errorAttribute != null)
                return errorAttribute.error;

            throw new NotImplementedException("You should use SpecificationErrorAttribute to describe Specification error");
        }

        /// <summary>
        /// Get the ordered specification
        /// </summary>
        /// <typeparam name="T">Specification Entity</typeparam>
        /// <param name="specifications">Specifications</param>
        /// <returns></returns>
        public static IList<Specification<T>> SortSpecifications<T>(this IList<Specification<T>> specifications)
        {
            var dic = new Dictionary<int, Specification<T>>();
            foreach (var spec in specifications)
            {
                var attribute = (spec.GetType().GetCustomAttributes(typeof(SpecificationSortAttribute), true).FirstOrDefault()) as SpecificationSortAttribute;

                dic.Add(GetUniqueSort(attribute?.Sort, dic), spec);
            }

            return dic.OrderBy(x => x.Key).Select(x => x.Value).ToList();
        }

        private static int GetUniqueSort<T>(int? sort, Dictionary<int, Specification<T>> dic)
        {
            if (dic.Count == 0)
                return sort ?? 0;

            if (dic.Any(x => x.Key == (sort ?? 0)))
                return dic.Max(x => x.Key) + 1;

            return sort ?? 0;
        }
    }
}