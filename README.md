# FluentSpecification
![FluentSpecification](https://github.com/dzfweb/FluentSpecification/raw/master/ico/ico.png)

A small validation library for .NET that uses a fluent interface for building validations using Specification Pattern

#### In computer programming, the specification pattern is a particular software design pattern, whereby business rules can be recombined by chaining the business rules together using boolean logic. The pattern is frequently used in the context of domain-driven design.

## Benefits
- Easy to implement unit test, once you can test all specification separately;
- Easy to understand all business validation, once is more readable than a lot of "ifs" in one same code block;
- Easy to refactoring, once you can refactor only the one specification without interfere the others;


## Instalation

### Package Manager
`Install-Package DzfWeb.FluentSpecification`

### .NET CLI
`dotnet add package DzfWeb.FluentSpecification`


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

#### 5- Get all broken specifications

```
foreach(var item in _personValidator.InvalidRules)
{
    //item correspond to SpecificationErrorAttribute value defined on specification file
    Console.WriteLine(item);
}
```

## Customize the validation

#### You can filter which specifications you want to use in some cases
```
var isValidToSendEmail = _personValidator
                        .FilterRules(typeof(PersonEmailSpecification))
                        .IsValid(person);

```

#### You can add parameters to be used during the validation
```
var isValidToCreate = _personValidator
                .AddParameter("RestrictedEmail", "douglas.franco@dzfweb.com.br")
                .IsValid(person);
```

```
[SpecificationError(PersonValidation.InvalidEmail)]
public class PersonEmailSpecification : Specification<Person>
{
    public override bool IsSatisfiedBy(Person entity) =>
        !string.IsNullOrEmpty(entity.Email) && 
        new EmailAddressAttribute().IsValid(entity.Email) &&
        entity.Email != (string)Parameters.GetValueOrDefault("RestrictedEmail", "");
}
```





## Samples
For more samples, visit:
https://github.com/dzfweb/FluentSpecification/blob/master/FluentSpecification/FluentSpecification.Test/PersonTest.cs