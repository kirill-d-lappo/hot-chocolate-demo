[assembly: Module("HCDemoTypes")]
[assembly: DataLoaderModule("HCDemoDataLoader")]
[assembly:
  DataLoaderDefaults(
    AccessModifier = DataLoaderAccessModifier.PublicInterface,
    ServiceScope = DataLoaderServiceScope.DataLoaderScope
  )]
