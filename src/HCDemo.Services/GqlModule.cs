using HotChocolate;

// [assembly: Module("HCDemoServiceTypes")]
[assembly: DataLoaderModule("HCDemoServiceDataLoaders")]
[assembly:
  DataLoaderDefaults(
    AccessModifier = DataLoaderAccessModifier.PublicInterface,
    ServiceScope = DataLoaderServiceScope.DataLoaderScope
  )]
