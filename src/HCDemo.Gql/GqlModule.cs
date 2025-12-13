[assembly: Module("HCDemoGqlTypes")]
[assembly: DataLoaderModule("HCDemoGqlDataLoader")]
[assembly:
  DataLoaderDefaults(
    AccessModifier = DataLoaderAccessModifier.PublicInterface,
    ServiceScope = DataLoaderServiceScope.DataLoaderScope
  )]
