// gulpfile.js
const gulp = require("gulp");
const fs = require('fs');
const path = require('path');

function getTELFrontEndPackageVersion() {
    //Path to solution, its a centralised solution
    const packageJsonPath = path.join(__dirname, "..", "node_modules", "nhse-tel-frontend", "package.json");

    console.log("Attempting to read package.json at:", packageJsonPath);

    try {
        if (fs.existsSync(packageJsonPath)) {
            const packageJsonContent = fs.readFileSync(packageJsonPath, "utf8");
            const packageJson = JSON.parse(packageJsonContent);
            console.log("Successfully read package.json. Version:", packageJson.version);
            return packageJson.version;
        } else {
            console.warn("Package.json file does not exist at:", packageJsonPath);
            return null; // Return null if file not found
        }
    } catch (error) {
        console.error("Error reading or parsing package.json:", error);
        return null; // Return null on error
    }
}

// Task that adds a second public static string to existing file that provides the TELFrontend.css version
gulp.task("add-telFrontEndVersion-to-versionInfo", function (done) {
    const versionFilePath = "TELBlazorPackageVersion/VersionInfo.TELFrontEnd.cs";
    const TELFrontEndPackageVersion = getTELFrontEndPackageVersion();

    if (TELFrontEndPackageVersion === null) {
        console.error("Could not determine TELFrontEndPackageVersion. Aborting task.");
        return done(new Error("Failed to get TELFrontEndPackageVersion")); // Indicate task failure
    }

    console.log(`Adding TELFrontEndPackageVersion to CS file at: ${versionFilePath}`);


    // Read existing content
    let existingContent = fs.readFileSync(versionFilePath, "utf8");
    console.log("Existing content:", existingContent);

    const content = ` namespace TELBlazor.Components.TELBlazorPackageVersion{public static partial class VersionInfo{public static readonly string TELFrontEndPackageVersion = "${TELFrontEndPackageVersion}";}} `;

    // Write or overwrite the file completely
    fs.writeFileSync(versionFilePath, content, "utf8");
    console.log(`VersionInfo.TELFrontEnd.cs file updated with version ${TELFrontEndPackageVersion}`);

    done();
});


gulp.task("default", gulp.series("add-telFrontEndVersion-to-versionInfo"));