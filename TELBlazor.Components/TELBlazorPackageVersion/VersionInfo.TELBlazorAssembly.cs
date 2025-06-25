namespace TELBlazor.Components.TELBlazorPackageVersion
{
    public static partial class VersionInfo
    {
        //its not the package version its the assembly so no text at the end
        public static readonly string TELBlazorPackageAssemblyVersion = typeof(_Imports).Assembly.GetName().Version?.ToString() ?? "Unknown";
    }
}
