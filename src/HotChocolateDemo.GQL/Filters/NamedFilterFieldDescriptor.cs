using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Data.Filters;
using HotChocolate.Types.Descriptors;

namespace HotChocolateDemo.GQL.Filters;

public class NamedFilterFieldDescriptor : FilterFieldDescriptor
{
  protected NamedFilterFieldDescriptor(IDescriptorContext context, string scope, string fieldName) : base(
    context,
    scope,
    fieldName
  )
  {
  }

  protected NamedFilterFieldDescriptor(IDescriptorContext context, string scope, MemberInfo member) : base(
    context,
    scope,
    member
  )
  {
  }

  protected NamedFilterFieldDescriptor(IDescriptorContext context, string scope, Expression expression) : base(
    context,
    scope,
    expression
  )
  {
  }

  // main reason for this class to exist
  protected NamedFilterFieldDescriptor(IDescriptorContext context, string scope, Expression expression, string name) :
    base(context, scope, expression)
  {
    Definition.Name = name;
  }

  protected internal NamedFilterFieldDescriptor(IDescriptorContext context, string scope) : base(context, scope)
  {
  }

  // main reason for this class to exist
  public static NamedFilterFieldDescriptor New(
    IDescriptorContext context,
    string scope,
    Expression expression,
    string name
  )
  {
    return new NamedFilterFieldDescriptor(context, scope, expression, name);
  }
}
