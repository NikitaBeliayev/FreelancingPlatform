# Getting started with freelancing platform

## This is the home page of the freelance exchange project documentation, below is a quick navigation through the different sections of the wiki

1. [Project overview](#Project-overview)
2. [Technical overview](#Technical-overview)
3. [Code standards](#Code-standards)
4. [Code structure](#Code-structure)

## Project overview

The freelancing platform allows students to complete various projects for clients.
Projects can range from IT tasks to design, multimedia, marketing, training, and mentoring.
Payments will be made using a proprietary blockchain-based currency.

Initially, clients will be EPAM School of Digital Engineering faculty, 
but will later include external clients and students from EPAM and possibly all EHU students.
A rating system will help clients select the best freelancer, with customers able to leave public comments on completed work. 
New profiles will have a default rating, to be determined later.


## Technical overview

- .NET 8
- ASP.NET Core 8
- Entity Framework Core 8
- PostgreSQL
- MediatR
- Clean Architecture
- CQRS
- Docker
- Docker Compose

## Code standards

### Quick links for navigating through the technical description

- [Code style convention](#code-style-convention)
- [General rules for code style and identifier naming](#general-rules-for-code-style-and-identifer-naming)

### Code style convention

| Object name         | Notation   | Plural | Prefix | Underscores | Suffix | Char mask   |
|---------------------|------------|--------|--------|-------------|:------:|-------------|
| Namespace name      | PascalCase |   Yes  |   Yes  |      No     |   No   |  [A-z][0-9] |
| Class name          | PascalCase |   No   |   No   |      No     |   Yes  |  [A-z][0-9] |
| Constructor name    | PascalCase |   No   |   No   |      No     |   Yes  |  [A-z][0-9] |
| Method name         | PascalCase |   Yes  |   No   |      No     |   No   |  [A-z][0-9] |
| Method arguments    | camelCase  |   Yes  |   No   |      No     |   No   |  [A-z][0-9] |
| Local variables     | camelCase  |   Yes  |   No   |      No     |   No   |  [A-z][0-9] |
| Constants name      | PascalCase |   No   |   No   |      No     |   No   |  [A-z][0-9] |
| Field name Public   | PascalCase |   Yes  |   No   |      No     |   No   |  [A-z][0-9] |
| Field name Private  | _camelCase |   Yes  |   No   |     Yes     |   No   | _[A-z][0-9] |
| Delegate name       | PascalCase |   No   |   No   |      No     |   Yes  |    [A-z]    |
| Properties name     | PascalCase |   Yes  |   No   |      No     |   No   |  [A-z][0-9] |
| Enum type name      | PascalCase |   Yes  |   No   |      No     |   No   |    [A-z]    |
| Field name Internal | _camelCase |   Yes  |   No   |     Yes     |   No   | _[A-z][0-9] |

### General rules for code style and identifer naming

1. Use PascalCase for method and class naming.

```csharp
public class SomeClass
{
  public void FirstMethod()
  {
    //...
  }
  public int SecondMethod()
  {
    //...
  }
}
```

2.Name interfaces with a capital I.

```csharp
public interface ISomeInterface
{
  void SomeMethod();
}
```

3.Attribute types end with the word Attribute.

```csharp
public class SomeAttribute : System.Attribute
{
    private string Name;

    public AuthorAttribute(string name)
    {
        Name = name;
    }
}
```

4.Prefer clarity over brevity.

```csharp
public class ClarityOverBrevityExample
{
    // Avoid
    public int Calc(int a, int b) => a + b;

    // Correct
    public int CalculateSum(int operand1, int operand2)
    {
        return operand1 + operand2;
    }
}
```

5.Use Named Arguments in method calls:<br>
When calling a method, arguments are passed with the parameter name followed by a colon and a value.

```csharp
// Method
public void DoSomething(string foo, int bar) 
{
    //...
}

// Avoid
DoSomething("someString", 1);
// Correct
DoSomething(foo: "someString", bar: 1);
```

6.Use the async suffix when writing asynchronous methods

```csharp
//Avoid
public async Task SomeMethod()
{
    //...
}

//Correct
public async Task SomeMethodAsync()
{
    //...
}
```

7.Use Allman style braces.

```csharp
//Avoid
public void SomeMethod(){
    //...
}

//Correct
public void SomeMethod()
{
    //...
}
```

8.Avoid using single-letter names, except for simple loop counters.<br>
Syntax examples are an exception to the rule.

- Use **S** for structs, C for classes.
- Use **M** for methods.
- Use **v** for variables, p for parameters.
- Use **r** for ref parameters.

```csharp

public int SomeMethod(int operand1, int operand2)
{
    //Avoid
    int a = operand1 + operand2
    return a;
}

//Correct
public int SomeMethod(int operand1, int operand2)
{
    //Avoid
    int sum = operand1 + operand2
    return sum;
}
```

9.Do declare all member variables at the top of a class, with static variables at the very top.

```csharp
// Correct
public class Account
{
  public static string BankName;
  public static decimal Reserves;      
  public string Number { get; set; }
  public DateTime DateOpened { get; set; }
  public DateTime DateClosed { get; set; }
  public decimal Balance { get; set; }     
  // Constructor
  public Account()
  {
    // ...
  }
}
```

10.Do use noun or noun phrases to name a class.

```csharp
public class Employee
{
}
public class BusinessLocation
{
}
public class DocumentCollection
{
}
```

11.Do not use Screaming Caps for constants or readonly variables.

```csharp
// Correct
public const string ShippingType = "DropShip";
// Avoid
public const string SHIPPINGTYPE = "DropShip";
```

12.Do not use an "Enum" suffix in enum type names.

```csharp
// Don't
public enum CoinEnum
{
  Penny,
  Nickel,
  Dime,
  Quarter,
  Dollar
} 
// Correct
public enum Coin
{
  Penny,
  Nickel,
  Dime,
  Quarter,
  Dollar
}
```

### Rules for describing commits and naming branches

### Branches

- Use meaningful branch names that capture the essence of what you are changing in that branch.

```git
//Avoid
resolve-problem
//Correct
add-logging-system
```

- Start the branch name with the number of the problem you will be solving in this branch. Also, at the beginning of the thread, explain what you have done, using prefixes: feat, fix, hotfix, release.

```git
//Avoid
resolve-problem
//Correct
fix/user-validation-rules
```

-Use dashes between words in a branch name

```git
//Avoid
resolveproblem
//Correct
user-validation-rules
```

- Once a branch is created, it needs to be attached to the issue you are solving.

### Commits

- The commit message should be brief but informative. Describe the nature of the changes made.

```git
//Avoid
fix issue #19: Updated files
//Correct
fix issue #7: Add user authentication feature
```

- Each commit should make only one change. This improves the ability to track and undo changes.

```git
//Avoid
fix issue #3: Updated files and fix issue with form validation
//Correct
update issue #6: Update header styles (commit 1)
fix issue #39: Fix issue with form validation (commit 2)

```

- Use keywords in the commit message such as "fix", "feat", "update", "refactor" to explicitly state what the commit does.

```git
//Avoid
Changed structure
//Correct
feat issue #45:
Refactor database connection handling
```

- If your commit is related to a specific task in the task tracking system, add a link to it in the commit message.

```git
//Avoid
Add user authentication feature
//Correct
feat issue #45:
Add user authentication feature
```

### Official Reference

1. [GitHub](https://github.com/ktaranov/naming-convention/blob/master/C%23%20Coding%20Standards%20and%20Naming%20Conventions.md)
2. [MSDN General Naming conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names)
3. [Commit naming convention](https://www.conventionalcommits.org/en/v1.0.0/#:~:text=Commits%20MUST%20be%20prefixed%20with,to%20your%20application%20or%20library.)
4. [Branches creating rules](https://docs.gitlab.com/ee/user/project/repository/branches/)

## Code structure

1. src - all projects should be placed here

   1.1. Domain - place of all Domain models living

   1.2. FreelancingPlatform - Asp.Net Core API. All Controllers here + Dependency Injection + application config

   1.3. Application - a place where cases should be put in. Also, it takes any requesters from the FreelancingPlatform, 
   manages Domane models calling, and returns a response. The MediatR types are living here. Also, <...>DTOs will be here.

   1.4. Infrastructure - real implementation of interfaces (IRepository etc.) + external providers (email)
2. tests - all projects from 'scr' must be covered by unit tests. As a result, this folder contains all test projects
3. Solution items - all nested ci/cd configurations, docker-compose files, etc.