# Dexih.Utils.DataType

[build]:    https://ci.appveyor.com/project/dataexperts/dexih-utils-datatype 
[build-img]: https://ci.appveyor.com/api/projects/status/468vuvi0rcpcs0ca?svg=true
[nuget]:     https://www.nuget.org/packages/dexih.utils.datatype
[nuget-img]: https://badge.fury.io/nu/dexih.utils.datatype.svg
[nuget-name]: dexih.utils.datatype
[dex-img]: http://dataexpertsgroup.com/assets/img/dex_web_logo.png
[dex]: https://dataexpertsgroup.com

[![][dex-img]][dex]

[![Build status][build-img]][build] [![Nuget][nuget-img]][nuget]

The `DataType` library provides dynamic datatype conversion functions and a high performing alternative to the `object`type when dealing with data types that are unknown at compile time.  This is useful when creating routines to process files or database records where the data type are not known, or for creating generic libraries that need to perform calculations on any datatype.

The primary benefits:

 * Perform conditional functions (equal, greater than, etc) on variables of different types.
 * Perform arithmetic (add, subtract, multiply, divide) on variables of different types.
 * Intelligently parse data types.  For example string "off" will parse to boolean false.
 * Parse arrays of unknown data types into a consistent data type.
 * A datatype which stores any types and can used for operations without the [boxing/unboxing](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing) overhead of the `object` type.
---

## Installation

Add the [latest version][nuget] of the package "dexih.utils.datatype" to a .net core/.net project.  This requires .net standard framework 2.0 or newer, or the .net framework 4.6 or newer.

---

## Dynamic Parsing

To parse any value, use the `Parse` method.  The method has a number of syntaxes which can be used.

Syntax 1: object Parse(ETypeCode typeCode, object inputValue)

```csharp
// returns true
var result = Operations.Parse(ETypeCode.Boolean, "true");
```

Syntax 2: object Operations<T>.Parse.Value(a);

```csharp
// return int 123
var result = Operations<int>.Parse.Value("123");
```

Syntax 3: object Parse(Type type, object inputValue)
```csharp
// return true
var result = Operations.Parse(typeof(boolean), "true");
```

Syntax 4: object Parse(Type type, int rank, object inputValue)
```csharp
// return new int[] {1,2,3}
result = Operations.Parse(ETypeCode.Int32, 1, new string[] {"1", "2", "3"});
```

## Dynamic Operations

The library supports a number of operations against variables when the datatype is not known a build time.

The following table shows a number of methods to add two values, and the performance tradeoff of each.

|Method|Syntax|Type Known at Build|Performance|Rank
|-|-|-|-|-|
|Inline add|`int1 + int2`|yes|0.0100 ns|1|
|Unbox add|`(T) value1 + (T) value2`|yes| 1.0632 ns|2|
|Operations Add Known Types|`Operations.Add<T>(T value1, T value1)`|yes| 3.5170 ns|3|
|GenericMath Library|`Generic.Math.GenericMath.Add((int)value1, (int)value2)`|yes|5.6363 ns|4|
|DataValue Add (returns object)|`dataValue1.Add(dataValue2)`|no|12.2334 ns|5|
|Dynamic Add|`((dynamic) value1) + ((dynamic) value2)`|no|15.4083 ns|6|
|DataValue + (returns new DataValue)|`dataValue1 + dataValue2`|no| 23.8452 ns|7|
|Operations Add Objects with eTypeCode|`Operations.Add(ETypeCode type, object value1, object value1)`|no|40.4726 ns|8|
|Operations Add Objects|`Operations.Add(object value1, object value1)`|no|137.8605 ns|8|

* Operations (such as Add) are clearly significantly faster using inline operations, or operations using generic versions of the functions.
* When the values are not known have overheads due to the need to use reflection functions to determine the underlying types.

Some example of the types of operations available:

```csharp
object v1 = 4;
object v2 = "2";

// returns -4
result = Operations.Negate(v1);

// returns 5
result = Operations.Increment(v1);

// returns 3
result = Operations.Decrement(v1);

// returns 6
result = Operations.Add(v1, v2);

// returns 2
result = Operations.Subtract(v1, v2);

// returns 8
result = Operations.Multiply(v1, v2);

// returns 2
result = Operations.Divide(v1, v2);

// returns 0.04
result = Operations.DivideInt(v1, 100);

// returns true
result = Operations.GreaterThan(v1, v2);

// returns false
result = Operations.LessThan(v1, v2);

// returns false
result = Operations.LessThanOrEqual(v1, v2);

// returns true
result = Operations.GreaterThanOrEqual(v1, v2);

// returns false
result = Operations.Equal(v1, v2);

// returns 0 if equal, -1 if less, 1 if greater.
result = Operations.Compare(v1, v2);
```

## Dynamic Storage

The library contains two objects which can be used for dynamic storage of unknown data types.

* DataValue - Stores only [Value Types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value-types) and can be used to perform fast operations such as arithmetic and comparisons.  This uses a `struct` which is stored on the stack and not subject to garbage collection, making it idea for long running and intense processing tasks.
* DataObject - Stores any value data type, and allows operations like the `DataValue`.  The disadvantage of this construct is there is a performance consideration due to allowing non ValueType values.,

Data Value Example:

```csharp
var dataValue1 = new DataValue(10);
var dataValue2 = new DataValue(5);

// returns 15
var result = dataValue1.Add(dataValue2);
```

DataObject example:

```csharp
var dataObject1 = new DataObject("abc");
var dataObject2 = new DataObject("def"));

// returns false
var result = dataObject1 > dataObject2

```

## Issues and Feedback

This library is provided free of charge under the MIT licence and is actively maintained by the [Data Experts Group](https://dataexpertsgroup.com)

Raise issues or bugs through the issues section of the git hub repository ([here](https://github.com/DataExperts/Dexih.Utils.ManagedTasks/issues)).  

Pull requests are welcomed.

