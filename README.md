# GenericBuilder
A quick console application to generate generic classes in C# from a template.

I made a generic class containing from 1 to 8 parameters at work before I decided to make this.

## Running

1. Drag one or more template files onto the built **GenericBuilder.exe**.

2. If the template file(s) does not include a header to specify the amount of generic parameters to generate, the program will prompt you to input the desired amount.

3. The output file is saved at the same location as the template with `_output` appended to the file name before the extension.

## Templates

[Sample Template](TemplateTest.txt)

```
public class TestClass<#>()
{
    public Action<#> Action;

    public TestClass(Action<#> action)
    {

    }

    public void Invoke()
    {

    }

    public void Invoke(#args)
    {

    }
}
```

There are two identifiers used to generate the resulting code from the template.

- **<#>** - This identifies generic parameters (i.e. <T1, T2, ..., T<sub>n</sub>>)

- **#args** - This identifies generic arguments (i.e. T1 arg1, T2 arg2, ..., T<sub>n</sub> arg<sub>n</sub>)

If you wish to do so, the prompt for the number of parameters can be avoided by appending **#<sub>n</sub>** to the top of the file, where **n** is the desired amount of generic parameters to generate.

For example:

```
#4
public class TestClass<#>()
{
    public Action<#> Action;

    public TestClass(Action<#> action)
    {

    }

    public void Invoke()
    {

    }

    public void Invoke(#args)
    {

    }
}
```

The [resulting output file](TemplateTest_output.txt) will contain:

```
public class TestClass<T1>()
{
	public Action<T1> Action;

	public TestClass(Action<T1> action)
	{

	}

	public void Invoke()
	{

	}

	public void Invoke(T1 arg1)
	{

	}
}

public class TestClass<T1, T2>()
{
	public Action<T1, T2> Action;

	public TestClass(Action<T1, T2> action)
	{

	}

	public void Invoke()
	{

	}

	public void Invoke(T1 arg1, T2 arg2)
	{

	}
}

public class TestClass<T1, T2, T3>()
{
	public Action<T1, T2, T3> Action;

	public TestClass(Action<T1, T2, T3> action)
	{

	}

	public void Invoke()
	{

	}

	public void Invoke(T1 arg1, T2 arg2, T3 arg3)
	{

	}
}

public class TestClass<T1, T2, T3, T4>()
{
	public Action<T1, T2, T3, T4> Action;

	public TestClass(Action<T1, T2, T3, T4> action)
	{

	}

	public void Invoke()
	{

	}

	public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
	{

	}
}
```

## Contributing/Requests

Simply submit a pull-request or submit an issue if you want any feature or use-case implemented.
