# :leaves: LeafS language
**LeafS** - powerful programming language with builtin tools for game developers.

Project in active development.

## Quick overview

### Math expressions

LeafS has 6 math operations:  ```+ - * / % ^```

Examples (```->``` - input, ```>>``` - output):

```
-> 1 + 2
>> 3
-> 2 * 4
>> 8
-> 2 ^ 123
>> 1,063382E+37
-> 3 % 2
>> 1
```

All numbers in LeafS are float-typed:

```
-> 1.1 + 2.45
>> 3,55
-> 123.123123 - 321.3312
>> -198,2081
```

It also support braces:
```
-> 2 * (2 + 2)
>> 8
-> (213 * (2 - 5677) + (32 * 54)) - 12345
>> -1219392
```

## Building

Simply clone this repo and open ```.sln``` file by Visual Studio.

_Note: project file is for **Visual Studio 2017**. If you want to open project in older version, please open ```.csproj``` file instead._

After that, press Build and Run green button on the top (or Ctrl+F5 to run without debugging, F5 to debug)

If compilation will be fine, you will se window like that:

![GitHub Logo](/images/screenshot.png)

## Testing

For now, all tests are hardcoded in Testing/UnitTests.cs file.

It has some testing methods:
- ```TestExpressions()``` - run tests in console for math expressions
