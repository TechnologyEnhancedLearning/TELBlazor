namespace TELBlazor.Components.TELBlazorPackageVersion
{
    public static partial class VersionInfo
    {
        public static readonly string TELBlazorPackageVersion = typeof(_Imports).Assembly.GetName().Version?.ToString() ?? "Unknown";
    }
}
