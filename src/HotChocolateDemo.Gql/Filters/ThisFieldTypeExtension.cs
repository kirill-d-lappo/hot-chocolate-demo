using System.Linq.Expressions;
using HotChocolate.Data.Filters;

namespace HotChocolateDemo.Gql.Filters;

public static class ThisFieldTypeExtension
{
  public static IFilterFieldDescriptor ThisField<T>(this IFilterInputTypeDescriptor descriptor)
  {
    var extend = descriptor.Extend();
    Expression<Func<T, T>> thisExpr = v => v;
    var fieldDescriptor = NamedFilterFieldDescriptor.New(extend.Context, extend.Definition.Scope, thisExpr, "_this");

    extend.Definition.Fields.Add(fieldDescriptor.CreateDefinition());

    return fieldDescriptor;
  }
}
