// Required in order to use the addin Cake.Paket because it downloads paket.exe.
#tool nuget:?package=Paket

// Required in order to use PaketRestore, PaketPack, and PaketPush.
#addin nuget:?package=Cake.Paket

Task("Sync-Paket")
.Does(() =>
{
    StartProcess(@"./tools/Paket/tools/paket.exe", new ProcessSettings{
        Arguments = "install"
    });
});

// Restores packages
Task("Paket-Restore").Does(() =>
{
    PaketRestore();
});

// Creates a nuget package
Task("Paket-Pack").Does(() =>
{
    PaketPack("./NuGet");
});

Task("Say-Hello-World")
.Does(() =>
{
    StartProcess(@"./paket-files/www.python.org/python.exe", new ProcessSettings{
        Arguments = "./scripts/python/helloworld/__init__.py"
    });
});


Task("Default")
.IsDependentOn("Sync-Paket")
.IsDependentOn("Paket-Restore")
.IsDependentOn("Say-Hello-World");

RunTarget("Default")