using HotChocolate;

[assembly: Module("HCDemoServiceTypes")]
[assembly: DataLoaderModule("HCDemoServiceDataLoader")]
[assembly:
  DataLoaderDefaults(
    AccessModifier = DataLoaderAccessModifier.PublicInterface,
    ServiceScope = DataLoaderServiceScope.DataLoaderScope
  )]
