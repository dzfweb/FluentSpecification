# FluentSpecification
A small validation library for .NET that uses a fluent interface for building validations using Specification Pattern

#### In computer programming, the specification pattern is a particular software design pattern, whereby business rules can be recombined by chaining the business rules together using boolean logic. The pattern is frequently used in the context of domain-driven design.

## Benefits
- Easy to implement unit test, once you can test all specification separately;
- Easy to understand all business validation, once is more readable than a lot of "ifs" in one same code block;
- Easy to refactoring, once you can refactor only the one specification without interfere the others;

## Usage


#### 1- Given a Entity
```
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
```

#### 2- Create all the specifications
For each business validation, create a new specification file with the validation
```
[SpecificationError(PersonValidation.InvalidName)]
public class PersonNameSpecification : Specification<Person>
{
    public override bool IsSatisfiedBy(Person entity) =>
        !string.IsNullOrEmpty(entity.FirstName) &&
        !string.IsNullOrEmpty(entity.LastName);
}
``` 

```
[SpecificationError(PersonValidation.InvalidEmail)]
public class PersonEmailSpecification : Specification<Person>
{
    public override bool IsSatisfiedBy(Person entity) =>
        !string.IsNullOrEmpty(entity.Email) && 
        new EmailAddressAttribute().IsValid(entity.Email);
}
```

#### 3- Create a validator
Create a validator grouping all the specifications 
```
public class PersonValidator : Validator<Person>
{
    public PersonValidator(PersonNameSpecification personNameSpecification,
                            PersonEmailSpecification personEmailSpecification) 
        : base(personNameSpecification,
              personEmailSpecification)
    { }
}
```

#### 4- Use the validator
```
var result = _personValidator.IsValid(person);
```

## Samples
For more samples, visit:
https://github.com/dzfweb/FluentSpecification/blob/master/FluentSpecification/FluentSpecification.Test/PersonTest.cs